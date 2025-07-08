using System;
using UnityEngine;

[RequireComponent(typeof(Mover),
                  typeof(ResourcePicker))]
public class Unit : MonoBehaviour, IPoolable<Unit>
{
    [SerializeField] private Base _base;
    private Mover _mover;
    private ResourcePicker _picker;
    private UnitStateMachine _stateMachine;

    public event Action<Unit> Destroyed;

    [field: SerializeField] public Resource AssignedResource { get; private set; }
    [field: SerializeField] public bool IsBusy => AssignedResource != null;
    public Vector3 StartPosition { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _picker = GetComponent<ResourcePicker>();

        _stateMachine = new(this, _mover, _picker);
    }

    private void OnEnable() =>    
        _stateMachine.SetState<IdleState>();

    private void Update() =>
        _stateMachine.Update();

    private void OnDisable() =>    
        Destroyed?.Invoke(this);    

    public void SetStartPosition(Vector3 position)
    {
        StartPosition = position;

        _mover.Warp(position);
    }

    public void ClearResource()
    {
        _picker.DropResource();
        AssignedResource = null;
    }
    public Vector3 GetPositionBase() =>
         _base.transform.position;

    public void AssignResource(Resource resource) =>
        AssignedResource = resource;

    public void SetBase(Base @base) =>
        _base = @base;

    public void DeliverResource() =>
        _base.CollectResource(AssignedResource);
}