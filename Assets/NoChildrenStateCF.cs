public class NoChildrenStateCF : State<ChildrenFollower>
{
    public NoChildrenStateCF(ChildrenFollower context) : base(context) { }

    public override void Enter()
    {
        context.StopFollowing();
        context.particle.SetActive(true);
        context.UI.SetActive(true);
    }
}