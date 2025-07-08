using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string SpeedParameter = "Speed";

    private Animator _animator;

    private void Awake() =>   
        _animator = GetComponent<Animator>();    

    public void MoveAnimation(float speed) =>   
        _animator.SetFloat(SpeedParameter, speed);    
}