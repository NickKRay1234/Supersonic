using UnityEngine;

public class FollowingStateCF : State<ChildrenFollower>
{
    public FollowingStateCF(ChildrenFollower context) : base(context) { }

    public override void Enter()
    {
        context.StartFollowing();
    }

    public override void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            context.SetState(new NotFollowingStateCF(context));
        }
    }
}