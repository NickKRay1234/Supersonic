public class InitialState : State<FollowInput>
{
    public InitialState(FollowInput context) : base(context) { }

    public override void OnMouseDown()
    {
        context.SetState(new FollowingState(context));
    }

    public override void Exit()
    {
        context.StartAnimation.SetActive(false);
        context.ArrowsInstruction.SetActive(false);
    }
}