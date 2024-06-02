using System.Collections;
using UnityEngine;

public sealed class FollowInput : MonoBehaviour
{
    private bool _canFollow = true;
    private IEnumerator _move;
    private InputService _inputService;

    private void Awake() => ServiceLocator.RegisterService(this);
    public void Initialize(InputService inputService) => _inputService = inputService;

    private void OnMouseDown()
    {
        if (!_canFollow) return;
        _move = MoveToPosition();
        StartCoroutine(_move);
    }

    private void OnMouseUp()
    {
        if (_move != null)
            StopCoroutine(_move);
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

    public void SetCanFollow(bool canFollow) =>
        _canFollow = canFollow;

    public bool GetCanFollow() => _canFollow;
}