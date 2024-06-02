using DG.Tweening;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveDuration = 1f;

    private void Start() => StartMoving();

    void StartMoving()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y - moveDistance, moveDuration).SetEase(Ease.InOutQuad));
        sequence.Append(transform.DOMoveY(transform.position.y, moveDuration).SetEase(Ease.InOutQuad));
        sequence.SetLoops(-1);
    }
}
