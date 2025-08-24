using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animation;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    public void Walk()
    {
        _animation.SetFloat("Velocity", Mathf.Abs(_rigidbody.linearVelocity.x));
    }

    public void Idle()
    {
        _animation.SetTrigger("Idle");
    }

    public void Attack()
    {
        _animation.SetTrigger("Attack");
    }

    public void EnemyShoot(bool _IShooting)
    {
        _animation.SetBool("Shoot", _IShooting);
    }

    public void Hurt()
    {
        _animation.SetTrigger("Hurt");
    }

    public void Death()
    {
        _animation.SetBool("Death", true);
    }
}
