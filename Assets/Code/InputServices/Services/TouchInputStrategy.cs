using UnityEngine;

public class TouchInputStrategy : AbstractInput, IInputStrategy
{
    public Vector3 GetInputPosition()
    {
        if (Input.touchCount <= 0) return Vector3.zero;
        Vector3 touchScreenPosition = Input.GetTouch(0).position;
        return _camera.ScreenToWorldPoint(touchScreenPosition);
    }
}