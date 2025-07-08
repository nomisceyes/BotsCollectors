public interface IStateChanger
{
    public void SetState<TState>() where TState : UnitState;
}