using UnityEngine;

public class FollowingState : State<FollowInput>
{
    public FollowingState(FollowInput context) : base(context) { }

    public override void Enter()
    {
        context.StartFollowing();
    }

    public override void OnMouseUp()
    {
        context.SetState(new NotFollowingState(context));
    }
}