using UnityEngine;

public class AnimationService : MonoBehaviour, IAnimationService
{
    private readonly PulseEffect _pulse = new();
    
    private void Awake() => 
        ServiceLocator.RegisterService(this);

    public void PlayAnimation(Transform obj) => 
        _pulse?.StartPulse(obj);
}