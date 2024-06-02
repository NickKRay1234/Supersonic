using UnityEngine;

public class InputService
{
    private IInputStrategy _inputStrategy;

    public void SetInputStrategy(IInputStrategy inputStrategy) => 
        _inputStrategy = inputStrategy;

    public Vector3 GetInputPosition() => 
        _inputStrategy?.GetInputPosition() ?? Vector3.zero;
}