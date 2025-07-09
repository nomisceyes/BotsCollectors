using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, IPoolable<Resource>
{
    private Rigidbody _rigidbody;

    public event Action<Resource> Destroyed;

    [field: SerializeField] public bool IsReserved { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void IsPickUp()
    {
        _rigidbody.isKinematic = true;
    }

    public void MarkAsTaken()
    {
        Destroyed?.Invoke(this);
        IsReserved = false;
        _rigidbody.isKinematic = false;
    }

    public bool TryReserve()
    {
        if(IsReserved == false)
        {
            IsReserved = true;
            return true;
        } 

        return false;
    }
}