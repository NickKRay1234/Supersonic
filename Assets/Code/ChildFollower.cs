using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChildrenFollower : MonoBehaviour
{
    public VisibleRaycast2D raycastScript;
    public HumanSettings humanSettings;
    public Transform house;
    public GameObject particle;
    public GameObject UI;

    private ChildrenCreator childrenCreator;
    private bool _canFollow = false;
    public bool СanStartFollowing = false;
    private int startedChildrenCount = 0;
    private ISoundService _soundService;
    private IAnimationService _animationService;
    private State<ChildrenFollower> currentState;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
        SetState(new InitialStateCF(this));
    }

    private void Start()
    {
        childrenCreator = GetComponent<ChildrenCreator>();
        _animationService = ServiceLocator.GetService<AnimationService>();
        _soundService = ServiceLocator.GetService<SoundService>();
        
        ServiceLocator.GetService<FollowInput>().OnStopFollowing += () => СanStartFollowing = true;
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SetState(State<ChildrenFollower> newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void StartFollowing()
    {
        if (_canFollow) return;
        _canFollow = true;
        StartCoroutine(StartFollowingCoroutine());
    }

    public void StopFollowing()
    {
        _canFollow = false;
    }

    private IEnumerator StartFollowingCoroutine()
    {
        while (_canFollow && startedChildrenCount < childrenCreator.children.Count)
        {
            GameObject child = childrenCreator.children[startedChildrenCount];
            StartCoroutine(FollowPath(child));
            startedChildrenCount++;
            yield return new WaitForSeconds(humanSettings.startDelay);
        }

        if (startedChildrenCount >= childrenCreator.children.Count )
        {
            SetState(new NoChildrenStateCF(this));
        }
    }

    private IEnumerator FollowPath(GameObject child)
    {
        List<Vector3> points = raycastScript.GetRopePositions();
        points.Add(raycastScript.player.position);
        
        _soundService?.PlaySound(humanSettings.soundClip);
        _animationService?.PlayAnimation(house.transform);

        foreach (Vector3 targetPosition in points)
        {
            if (child == null) yield break;
            
            while (Vector3.Distance(child.transform.position, targetPosition) > humanSettings.positionThreshold)
            {
                if (child == null) yield break;
                
                child.transform.position = Vector3.MoveTowards(child.transform.position, targetPosition, humanSettings.movementSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}

