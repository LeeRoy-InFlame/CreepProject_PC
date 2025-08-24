using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inputs
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement vars")]
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        [Header("Setting")]
        [SerializeField] private float _jumpOffset;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Transform _groundColladerCenter;
        [SerializeField] private LayerMask _groundMask;

        [Header("Components")]
        [SerializeField] private Collider2D _playerGroundCollider;
        [SerializeField] private SoundController _soundController;

        private TakeControl _takeControl;
        private bool _isGrounded;
        private Rigidbody2D _rigidbody;
        private Vector2 _scale;
        private bool _isFacingRight;


        public bool IsGrounded => _isGrounded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _takeControl = GetComponent<TakeControl>();
            _isFacingRight = true;
            HorizontalMovement(0f);

        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundColladerCenter.position, _jumpOffset, _groundMask);

        }

        private void HorizontalMovement(float _direction)
        {
            _rigidbody.linearVelocity = new Vector2(_curve.Evaluate(_direction) * _speed, _rigidbody.linearVelocity.y);

            if (_isFacingRight == false && _direction > 0)
            {
                Turn();
            }

            if (_isFacingRight == true && _direction < 0)
            {
                Turn();
            }
        }

        private void Turn()
        {
            _isFacingRight = !_isFacingRight;
            _scale = transform.localScale;
            _scale.x *= -1;
            transform.localScale = _scale;
        }

        public void Move(float _direction, bool _isJumpButtonPressed, float _verticalDirection)
        {
            if (Mathf.Abs(_direction) > Mathf.Epsilon)
            {
                HorizontalMovement(_direction);
            }

            if (_isJumpButtonPressed == true && _verticalDirection >= 0)
            {
                Jump();
            }
        }

        public void Jump()
        {
            if (_isGrounded == true)
            {
                _soundController.JumpSound();
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            }
        }

        public void JumpingDown(float _verticalDirection, bool _isJumpButtonPressed)
        {
            if (_verticalDirection < 0 && _isJumpButtonPressed == true)
            {
                _playerGroundCollider.gameObject.SetActive(false);
            }

            if (_rigidbody.linearVelocityY < -1.5f)//�� ����� ����������� ��� ���������� ���� �������� ������� ��������� ������ ���������� �������
            {
                _playerGroundCollider.gameObject.SetActive(true);
            }
        }
    }
}



