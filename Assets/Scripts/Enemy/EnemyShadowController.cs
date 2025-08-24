using System.Collections;
using UnityEngine;

public class EnemyShadowController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Health _health;
    [SerializeField] private Collider2D _bodyCollider;
    [SerializeField] private float _minDistanceFromThePlayer;
    [SerializeField] private float _timeBackToNormal;

    private EnemyAnimationController _animationController;
    private SoundController _soundController;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody;
    private GameObject _player;
    private Transform _playerPosition;
    private Vector2 _direction;

    private bool _lookingRight = false;
    private Vector2 _scale;

    private const int WALK_STATE = 0;
    private const int ATTACK_STATE = 1;
    private const int DEATH_STATE = 2;
    private const int IDLE_STATE = 3;

    private int _currentState;



    private void Start()
    {
        _soundController = GetComponent<SoundController>();
        _sprite = GetComponent<SpriteRenderer>();
        _animationController = GetComponent<EnemyAnimationController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentState = WALK_STATE;
        _soundController.EnemySeePlayerSound();
        FindPlayer();
        
    }

    private void Update()
    {
        if (_player == null)
        {
            _playerPosition = GetComponent<Transform>();
            _currentState = IDLE_STATE;
        }
        _direction = _playerPosition.position - transform.position;
        if (_health.IsAlive == false)
        {
            _currentState = DEATH_STATE;
            
        }

        if (Mathf.Abs(_playerPosition.position.x - transform.position.x) >= _minDistanceFromThePlayer && _currentState != DEATH_STATE)
        {
            _currentState = WALK_STATE;
        }

        switch (_currentState)
        {
            case ATTACK_STATE:
                _animationController.Attack();
                break;

            case DEATH_STATE:
                _rigidbody.linearVelocity = Vector2.zero;
                _bodyCollider.gameObject.SetActive(false);
                _animationController.Death();
                break;
            case IDLE_STATE:

                break;

        }
    }

    private void FixedUpdate()
    {
        _direction.Normalize();
        _animationController.Walk();
        if (_currentState != DEATH_STATE)
        {
            if (_direction.x > 0 && _lookingRight == true)
            {
                Turn();
            }

            else if (_direction.x < 0 && _lookingRight == false)
            {
                Turn();
            }
        }
        
        switch (_currentState)
        {
            case WALK_STATE:

                _rigidbody.linearVelocityX = _direction.x * _speed;
                

                if (Mathf.Abs(_playerPosition.position.x - transform.position.x) <= _minDistanceFromThePlayer && _player != null)
                {
                    _currentState = ATTACK_STATE;
                }
                break;
        }
    }

    public void GettingHit()//запускается через UnityEvent _gettingDamage из скрипта Heaith.
    {
        _sprite.color = Color.gray;
        _bodyCollider.gameObject.SetActive(false);
        StartCoroutine(TimerBackToNormal());
    }

    private IEnumerator TimerBackToNormal()
    {
        yield return new WaitForSecondsRealtime(_timeBackToNormal);
        _bodyCollider.gameObject.SetActive(true);
        _sprite.color = Color.white;
    }

    private void Turn()
    {
        _lookingRight = !_lookingRight;
        _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void FindPlayer()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player != null)
        {
            _playerPosition = _player.transform;
            
        }
    }
}
