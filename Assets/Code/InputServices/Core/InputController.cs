using UnityEngine;

public class InputController : MonoBehaviour
{
    private FollowInput _followInput;

    private void Start()
    {
        _followInput = ServiceLocator.GetService<FollowInput>();
        var inputService = new InputService();
        
        if (Application.isMobilePlatform)
            inputService.SetInputStrategy(new TouchInputStrategy());
        else
            inputService.SetInputStrategy(new MouseInputStrategy());

        _followInput.Initialize(inputService);
    }
}