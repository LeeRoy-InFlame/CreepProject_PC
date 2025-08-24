using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [Header("Movement vart")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [Header("Setting")]
    [SerializeField] private float _jumpOffset;
    [SerializeField] private Transform _groundColliderCenter;
    [SerializeField] private LayerMask _groundMask;

    private TakeControl _takeControl;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;

    public bool IsGrounded => _isGrounded;

    private void Start()
    {
        _takeControl = GetComponent<TakeControl>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundColliderCenter.position, _jumpOffset, _groundMask);
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        _rigidbody.linearVelocityX = _speed;
    }

    public void Jump(bool _isJumpButtonPressed)
    {
        if (_isGrounded == true && _isJumpButtonPressed == true)
        {
            _rigidbody.linearVelocityY = _jumpForce;
        }
    }
    //public void Jump()
    //{
    //    if (_isGrounded == true)
    //    {
    //        _rigidbody.linearVelocityY = _jumpForce;
    //    }
    //}
}
