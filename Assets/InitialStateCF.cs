using UnityEngine;

public class InitialStateCF : State<ChildrenFollower>
{
    public InitialStateCF(ChildrenFollower context) : base(context) { }

    public override void Update()
    {
        if (context.СanStartFollowing && Input.GetMouseButtonDown(0))
        {
            context.SetState(new FollowingStateCF(context));
        }
    }
}