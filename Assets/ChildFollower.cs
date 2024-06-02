using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenFollower : MonoBehaviour
{
    public VisibleRaycast2D raycastScript;
    public HumanSettings humanSettings;
    public Transform house;

    private ChildrenCreator childrenCreator;
    private bool _canFollow = false;
    private int startedChildrenCount = 0;
    private ISoundService _soundService;
    private IAnimationService _animationService;

    private void Awake() => ServiceLocator.RegisterService(this);

    private void Start()
    {
        childrenCreator = GetComponent<ChildrenCreator>();
        _animationService = ServiceLocator.GetService<AnimationService>();
        _soundService = ServiceLocator.GetService<SoundService>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_canFollow) return;
            _canFollow = true;
            StartCoroutine(StartFollowing());
        }
        else
        {
            _canFollow = false;
        }
    }

    private IEnumerator StartFollowing()
    {
        while (_canFollow && startedChildrenCount < childrenCreator.children.Count)
        {
            GameObject child = childrenCreator.children[startedChildrenCount];
            StartCoroutine(FollowPath(child));
            startedChildrenCount++;
            yield return new WaitForSeconds(humanSettings.startDelay);
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
