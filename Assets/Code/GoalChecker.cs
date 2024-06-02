using System.Collections.Generic;
using UnityEngine;

public sealed class GoalChecker : MonoBehaviour
{
    public SoundService _soundService;
    public AnimationService _animationService;
    public HumanSettings humanSettings;
    public Transform house;
    private List<Transform> humanChildren = new();
    
    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out FollowInput followMouse)) 
            followMouse.SetCanFollow(false);

        if (!other.TryGetComponent(out Human human)) return;

        _soundService.PlaySound(humanSettings.soundClip);
        _animationService.PlayAnimation(house.GetChild(0).transform);
        
        //other.transform.SetParent(house.GetChild(1).transform);
        //humanChildren.Add(other.transform);
        
        //ArrangeHumans();
    }
    
    private void ArrangeHumans()
    {
        for (int i = 0; i < humanChildren.Count; i++)
        {
            if(humanChildren[i] == null) continue;
            Vector3 newPosition = house.GetChild(1).position;
            newPosition.x += i * 0.125f;
            humanChildren[i].localPosition = newPosition;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out FollowInput followMouse)) 
            followMouse.SetCanFollow(true);
    }
}