public abstract class State<T>
{
    protected T context;

    protected State(T context)
    {
        this.context = context;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void OnMouseDown() { }
    public virtual void OnMouseUp() { }
}