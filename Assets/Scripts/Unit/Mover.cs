using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),
                  typeof(UnitAnimator))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private NavMeshAgent _agent;
    private UnitAnimator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<UnitAnimator>();
    }

    private void Start() =>    
        _agent.speed = _moveSpeed;
    
    private void Update() =>    
        _animator.MoveAnimation(_agent.velocity.sqrMagnitude);    

    public void Warp(Vector3 position) =>    
        _agent.Warp(position);

    public void Stop() =>    
        _agent.isStopped = true;    

    public void MoveTo(Vector3 target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target);    
    }
}