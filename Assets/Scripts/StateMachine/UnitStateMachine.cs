using System;
using System.Collections.Generic;

public class UnitStateMachine : IStateChanger
{
    private readonly Dictionary<Type, UnitState> _states = new();

    private UnitState _currentState;

    public UnitStateMachine(Unit unit, Mover mover, ResourcePicker picker)
    {
        _states.Add(typeof(IdleState), new IdleState(unit, this));
        _states.Add(typeof(GatheringState), new GatheringState(unit, mover, picker, this));
    }

    public void Update() =>
        _currentState.Update();

    public void SetState<TState>() where TState : UnitState
    {
        if (_states.TryGetValue(typeof(TState), out UnitState nextState))
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter();
        }
    }
}