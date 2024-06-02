using DG.Tweening;
using UnityEngine;

public class PulseEffect
{ 
    private float pulseScale = 1.1f; 
    private float pulseDuration = 0.1f;
    private Vector3 initialScale = Vector3.zero;

    public void StartPulse(Transform obj)
    {
        if(initialScale == Vector3.zero) 
            initialScale = obj.localScale;
        
        obj.localScale = initialScale;

        Sequence pulseSequence = DOTween.Sequence()
            .Append(obj.DOScale(initialScale * pulseScale, pulseDuration).SetEase(Ease.InOutSine))
            .Append(obj.DOScale(initialScale, pulseDuration).SetEase(Ease.InOutSine));
        
        pulseSequence.Play();
    }
}