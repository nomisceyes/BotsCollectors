using UnityEngine;

public class GatheringState : UnitState
{
    private const float INTERACT_DISTANCE = 4f;
    private const float BASE_DELIVERY_DISTANCE = 8f;

    private readonly Mover _mover;
    private readonly ResourcePicker _picker;

    public GatheringState(Unit unit, Mover mover, ResourcePicker picker, IStateChanger stateChanger) : base(unit, stateChanger)
    {
        _mover = mover;
        _picker = picker;
    }

    public override void Enter() =>
         MoveToResource();

    public override void Update()
    {
        if (Unit.AssignedResource == null)
        {
            StateChanger.SetState<IdleState>();
            return;
        }

        if (_picker.HasResource == false)
        {
            if (IsInRange(Unit.AssignedResource.transform.position, INTERACT_DISTANCE))
            {
                _picker.PickResource(Unit.AssignedResource);
                MoveToBase();
            }
        }
        else if (IsInRange(Unit.GetPositionBase(), BASE_DELIVERY_DISTANCE))
        {
            Unit.DeliverResource();
            Unit.ClearResource();
        }
    }

    public override void Exit() =>
        _mover.MoveTo(Unit.StartPosition);

    private void MoveToResource() =>
        _mover.MoveTo(Unit.AssignedResource.transform.position);

    private void MoveToBase() =>
        _mover.MoveTo(Unit.GetPositionBase());

    private bool IsInRange(Vector3 target, float range) =>
         (target - Unit.transform.position).sqrMagnitude <= range;
}