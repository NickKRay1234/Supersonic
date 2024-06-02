using UnityEngine;

public class MouseInputStrategy : AbstractInput, IInputStrategy
{
    public Vector3 GetInputPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        return _camera.ScreenToWorldPoint(mouseScreenPosition);
    }
}