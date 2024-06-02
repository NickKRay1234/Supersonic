using UnityEngine;

public class NotFollowingStateCF : State<ChildrenFollower>
{
    public NotFollowingStateCF(ChildrenFollower context) : base(context) { }

    public override void Enter()
    {
        context.StopFollowing();
    }

    public override void Update()
    {
        if (context.СanStartFollowing && Input.GetMouseButtonDown(0)) 
        {
            context.SetState(new FollowingStateCF(context));
        }
    }
}