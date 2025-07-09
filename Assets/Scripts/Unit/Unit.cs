using System;
using UnityEngine;

[RequireComponent(typeof(Mover),
                  typeof(ResourcePicker))]
public class Unit : MonoBehaviour, IPoolable<Unit>
{
    private Base _base;
    private Mover _mover;
    private ResourcePicker _picker;
    private UnitStateMachine _stateMachine;

    public event Action<Unit> Destroyed;

    [field: SerializeField] public Resource AssignedResource { get; private set; }
    [field: SerializeField] public bool IsBusy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _picker = GetComponent<ResourcePicker>();

        _stateMachine = new(this, _mover, _picker);
    }

    private void OnEnable() =>
        _stateMachine.SetState<IdleState>();

    private void Update()
    {
        if (AssignedResource != null)        
            IsBusy = true;       
        else      
            IsBusy = false;       

        _stateMachine.Update();
    }

    private void OnDisable() =>
        Destroyed?.Invoke(this);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public Vector3 GetPositionBase() =>
         _base.transform.position;

    public void AssignResource(Resource resource) =>
        AssignedResource = resource;

    public void SetBase(Base @base) =>
        _base = @base;

    public void DeliverResource()
    {
        _base.CollectResource(AssignedResource);
        ClearResource();
    }

    private void ClearResource()
    {
        _picker.DropResource();
        AssignedResource = null;
    }
}