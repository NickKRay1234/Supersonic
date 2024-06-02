using UnityEngine;

public class NotFollowingState : State<FollowInput>
{
    public NotFollowingState(FollowInput context) : base(context) { }

    public override void Enter()
    {
        context.StopFollowing();
        context.EndPointAnimation.SetActive(false);
    }
}