using System;
using System.Collections;
using UnityEngine;

public sealed class FollowInput : MonoBehaviour
{
    
    public event Action OnStopFollowing;
    
    private bool _canFollow = true;
    private IEnumerator _move;
    private InputService _inputService;
    private State<FollowInput> currentState;
    public GameObject StartAnimation;
    public GameObject EndPointAnimation;
    public GameObject ArrowsInstruction;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
        SetState(new InitialState(this));
    }

    public void Initialize(InputService inputService) => _inputService = inputService;

    private void OnMouseDown()
    {
        currentState.OnMouseDown();
    }

    private void OnMouseUp()
    {
        currentState.OnMouseUp();
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SetState(State<FollowInput> newState)
    {
        currentState?.Exit();
        currentState = newState;
        Debug.Log(newState.ToString());
        currentState.Enter();
    }

    public void StartFollowing()
    {
        if (!_canFollow) return;
        _move = MoveToPosition();
        StartCoroutine(_move);
    }

    public void StopFollowing()
    {
        if (_move != null)
            StopCoroutine(_move);
        OnStopFollowing?.Invoke();
    }

    private IEnumerator MoveToPosition()
    {
        while (true)
        {
            Vector3 inputPosition = _inputService.GetInputPosition();
            transform.position = new Vector3(inputPosition.x, inputPosition.y, transform.position.z);
            yield return null;
        }
    }

    public void SetCanFollow(bool canFollow) => _canFollow = canFollow;

    public bool GetCanFollow() => _canFollow;
}


