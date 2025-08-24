using UnityEngine;

public class EnemyWerewolfController : MonoBehaviour
{
    //враг преследует игрока и нападает, когда оказывается достаточно близко
    //происходит атака, затем небольшая пауза и вновь готовность к атаке
    [SerializeField] private float _speed;
    [SerializeField] private float _speedAttack;
    [SerializeField] private float _distanceOfAttack;
    [SerializeField] private float _distanceOfAggression;
    [SerializeField] private float _idleTimerMax;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _deathBody;

    private EnemyAnimationController _animation;
    private SoundController _soundController;
    private Rigidbody2D _rigidbody;
    private GameObject _player;
    private Transform _playerPosition;
    private Vector2 _direction;
    private Vector2 _scale;
    private bool _lookingRight;
    private bool _isHurt;
    private bool _isSawThePlayer = true;
    private bool _isAttack;
    private float _currentIdleTimer;
    private float _timeToNormalColor = 0.1f;

    private Color _normalSpriteColor;
    private SpriteRenderer _sprite;

    private int _currentState;
    private const int RUN_STATE = 0;
    private const int ATTACK_STATE = 1;
    private const int IDLE_STATE = 2;
    private const int NO_PLAYER_IDLE_STATE = 3;
    private const int DEATH_STATE = 4;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _soundController = GetComponent<SoundController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animation = GetComponent<EnemyAnimationController>();
        FindPlayer();
        _currentState = IDLE_STATE;
        _currentIdleTimer = _idleTimerMax;
        _normalSpriteColor = _sprite.color;
        _isHurt = false;
    }

    private void Update()
    {
        if (_player == null)
        {
            _currentState = NO_PLAYER_IDLE_STATE;
        }

        if (_health.IsAlive == false)
        {
            _currentState = DEATH_STATE;
        }

        if (_isHurt == true)
        {
            HurtBlinking();
        }

        _animation.Walk();

        switch (_currentState)
        {
            case NO_PLAYER_IDLE_STATE:
                _animation.Idle();
                _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocityY);
                break;

            case IDLE_STATE:
                _animation.Idle();
                _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocityY);

                if (Mathf.Abs(_playerPosition.position.x - transform.position.x) <= _distanceOfAggression)
                {
                    _currentIdleTimer -= Time.deltaTime;
                }
                
                if (_currentIdleTimer <= 0 && _health.IsAlive == true)
                {
                    if (_isSawThePlayer == true) //чтобы звук агрессии проигрывался один раз, когда игрок вошел в зону агрессии
                    {
                        _soundController.EnemySeePlayerSound();
                        _isSawThePlayer = false;
                    }
                    _currentState = RUN_STATE;
                    _currentIdleTimer = _idleTimerMax;
                    _isAttack = true;
                }
                break;

            case RUN_STATE:
                _direction = _playerPosition.position - transform.position;
                _direction.Normalize();
                _rigidbody.linearVelocityX = _direction.x * _speed;
                

                if (_rigidbody.linearVelocityX > 0 && _lookingRight == true)
                {
                    Turn();
                }

                else if (_rigidbody.linearVelocityX < 0 && _lookingRight == false)
                {
                    Turn();
                }

                if (Mathf.Abs(_playerPosition.position.x - transform.position.x) <= _distanceOfAttack)
                {
                    _currentState = ATTACK_STATE;
                }
                break;

            case ATTACK_STATE:
                
                if (_rigidbody.linearVelocity.x > 0)
                {
                    _rigidbody.linearVelocity = new Vector2 (_speedAttack, _rigidbody.linearVelocityY);
                }

                else if (_rigidbody.linearVelocity.x < 0)
                {
                    _rigidbody.linearVelocity = new Vector2(-_speedAttack, _rigidbody.linearVelocityY);
                }

                if (_isAttack == true) //чтобы анимация атаки не повторялась дважды
                {
                    _animation.Attack();
                    _isAttack = false;
                }
                break;

            case DEATH_STATE:
                _body.layer = LayerMask.NameToLayer("Ignore Raycast");
                _animation.Death();
                _rigidbody.linearVelocity = new Vector2(_direction.x * _speed/4, _rigidbody.linearVelocity.y);
                break;
        }
    }

    private void EndAttack()//вызывается в Анимации RunAndAttack
    {
        _currentState = IDLE_STATE;

    }

    private void Turn()
    {
        _lookingRight = !_lookingRight;
        _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void DontMove()//вызывается в Анимации DeathWerewolf
    {
        _direction.x = 0;
    }

    private void CreateDeathBody()
    {
        _deathBody = Instantiate(_deathBody, transform.position, Quaternion.identity);
    }

    public void Hurt()
    {
        _isHurt = true;
    }

    public void HurtBlinking()
    {
        _sprite.color = Color.black;
        _timeToNormalColor -= Time.deltaTime;
        if (_timeToNormalColor < 0)
        {
            _sprite.color = _normalSpriteColor;
            _timeToNormalColor = 0.1f;
            _isHurt = false;
        }
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
