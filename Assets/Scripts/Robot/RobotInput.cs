using UnityEngine;

namespace Player.Inputs
{
    public class RobotInput : MonoBehaviour
    {
        private RobotMovement _robotMovement;
        private RobotShoots _RobotShoots;
        private TakeControl _takeControl;

        private bool _isShooting;
        private bool _isJumpButtonPressed;

        public bool IsShooting => _isShooting;

        private void Start()
        {
            _robotMovement = GetComponent<RobotMovement>();
            _takeControl = GetComponent<TakeControl>();
            _RobotShoots = GetComponent<RobotShoots>();
        }

        private void Update()
        {
            if (_takeControl.ControlOff == false)
            {
                _isShooting = Input.GetButtonDown(GlobalStringVars.SHOOT);
                _isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);
                _robotMovement.Jump(_isJumpButtonPressed);
                _RobotShoots.Shot(_isShooting);
            }
        }


    }
}

