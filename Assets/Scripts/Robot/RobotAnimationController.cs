using UnityEngine;

namespace Player.Inputs
{
    public class RobotAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Rigidbody2D _rigidbody;
        private RobotMovement _robotMovement;
        private RobotInput _robotInput;

        private void Start()
        {
            _robotMovement = GetComponent<RobotMovement>();
            _robotInput = GetComponent<RobotInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Run();
            Jump();
            Shoot();
        }

        private void Run()
        {
            _animator.SetFloat("VelocityX", Mathf.Abs(_rigidbody.linearVelocityX));
        }

        private void Jump()
        {
            _animator.SetBool("IsGrounded", _robotMovement.IsGrounded);
        }

        private void Shoot()
        {
            _animator.SetBool("IsShooting", _robotInput.IsShooting);
        }
    }
}

