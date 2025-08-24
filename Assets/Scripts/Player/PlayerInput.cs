using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _move;

        private PlayerMovement _playerMovement;
        private PlayerShoot _playerShoot;
        private TakeControl _takeControl;
        private Rigidbody2D _rigidbody;

        private bool _isShooting = false;
        private bool _isJumpButtonPressed = false;
        private bool _isCancel;
        private float _horizontalDirection;
        private float _verticalDirection;

        public float HorizontalDirection => _horizontalDirection;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShoot = GetComponent<PlayerShoot>();
            _takeControl = GetComponent<TakeControl>();
        }

        private void OnEnable()
        {
            _move = Vector2.zero;
            _horizontalDirection = 0;
            _verticalDirection = 0;
            _isShooting = false;
            _isJumpButtonPressed = false;

            // ��������� ��������
            _rigidbody = GetComponent<Rigidbody2D>();
            if (_rigidbody != null)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
            }
        }


        private void Update()
        {
            if (_takeControl.ControlOff == false)
            {
                _horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
                _verticalDirection = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);
                _isShooting = Input.GetButtonDown(GlobalStringVars.SHOOT);
                _isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);
                _isCancel = Input.GetButtonDown(GlobalStringVars.ESCAPE);
                _playerMovement.Move(_horizontalDirection, _isJumpButtonPressed, _verticalDirection);
                _playerShoot.Shoot(_isShooting);
                _playerMovement.JumpingDown(_verticalDirection, _isJumpButtonPressed);
            }
        }
    }
}



