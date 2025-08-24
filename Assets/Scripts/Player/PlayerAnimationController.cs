using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inputs
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Rigidbody2D _rigidbody;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        { 
            RunAnimation();
            JumpAndFall();
        }

        public void Idle()
        {
            _animator.SetTrigger("Idle");
        }

        private void RunAnimation()
        {
            _animator.SetFloat("VelocityX", Mathf.Abs(_playerInput.HorizontalDirection));
            _animator.SetBool("IsGrounded", _playerMovement.IsGrounded);
        }

        public void Shoot()
        {
            _animator.SetTrigger("Shoot");
        }

        private void JumpAndFall()
        {
            _animator.SetFloat("VelocityY", _rigidbody.linearVelocity.y);
            _animator.SetBool("IsGrounded", _playerMovement.IsGrounded);
        }

        public void Hurt()
        {
            _animator.SetTrigger("Hurt");
        }
    }
}

