using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable<Resource>
{
    public event Action<Resource> Destroyed;

    [field: SerializeField] public bool IsReserved { get; private set; }

    //public void MarkAsBusy()
    //{
    //    IsReserved = true;
    //}

    public void MarkAsTaken()
    {
        Destroyed?.Invoke(this);
        IsReserved = false;
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

    private void OnDrawGizmos()
    {
        if (IsReserved) 
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawIcon(transform.position + Vector3.up * 2, "ResourceStatus");
    }
}