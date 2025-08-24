using UnityEngine;

public class EnemyInfectedSoldierController : MonoBehaviour
{
    //враг патулирует и при появлении игрока в области действия райкаста - начинает стрелять
    [SerializeField] private float _speed;
    [SerializeField] private float _raycastMaxDistance;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _timeToContinuePatrolling;
    [SerializeField] private Health _health;
    [SerializeField] private Transform _spawnSkullPosition;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private Transform[] _enemyPatrolPoints;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _skull;
    [SerializeField] private GameObject[] _dropItems;
    [SerializeField] private SoundAssetMenu _sounds;



    private RaycastHit2D _raycastHit;
    private EnemyAnimationController _animationController;
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _currentBulletRigidbody;
    private Vector2 _scale;
    private Vector2 _rayDirection;
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private GameObject _currentBullet;

    private SpriteRenderer _sprite;
    private Color _normalSpriteColor;

    private AudioSource _audioSource;

    private bool _isSeesThePlayer;
    private bool _lookingLeft;

    #region States
    private int _currentState;
    private const int IDLE_STATE = 0;
    private const int WALK_STATE = 1;
    private const int SHOOT_STATE = 2;
    private const int DEATH_STATE = 3;
    #endregion

    private float _timeToTurn;
    private float _currentTimeToTurn;
    private float _currentTimeToContinuePatrolling;

    #region ignorLayers
    private int _ignorLayer;
    private int _ignorLayerGround = 1 << 3;
    private int _ignorLayerBullet = 1 << 6;
    private int _ignorLayerEnemy = 1 << 8;
    private int _ignorLayerHarmless = 1 << 9;
    private int _ignorLayerPlayerGround = 1 << 10;
    private int _ignorLayerEnemyGround = 1 << 11;
    private int _ignorLayerEnemyBullet = 1 << 12;
    private int _ignorCameraConfiner = 1 << 14;
    #endregion

    private int _patrolPoint = 0;

    private void Start()
    {
        _currentState = IDLE_STATE;
        _timeToTurn = Random.Range(1f, 5f);
        _currentTimeToTurn = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<EnemyAnimationController>();
        _currentTimeToContinuePatrolling = _timeToContinuePatrolling;
        _sprite = GetComponent<SpriteRenderer>();
        _normalSpriteColor = _sprite.color;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _animationController.Walk();
        if (_currentTimeToTurn >= _timeToTurn && _health.IsAlive == true)
        {
            _currentState = WALK_STATE;
            _currentTimeToTurn = 0;
            _timeToTurn = Random.Range(1f, 5f);
        }

        if (_health.IsAlive == false)
        {
            _currentState = DEATH_STATE;
        }
        
        switch (_currentState)
        {
            case IDLE_STATE:
                _currentTimeToTurn += Time.deltaTime;
                break;
            
            case SHOOT_STATE:
                _animationController.EnemyShoot(_isSeesThePlayer);
                break;

            case DEATH_STATE:
                _rigidbody.linearVelocity = Vector2.zero;
                _skull = Instantiate(_skull, _spawnSkullPosition.position, Quaternion.identity);

                if (_dropItems != null)//дроп предмета при смерти врага
                {
                    Instantiate(_dropItems[Random.Range(0, _dropItems.Length)], transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case WALK_STATE:
                _moveDirection = (_enemyPatrolPoints[_patrolPoint].position - transform.position).normalized;
                _rigidbody.linearVelocity = new Vector2(_moveDirection.x * _speed, _rigidbody.linearVelocity.y);
                
                if (_rigidbody.linearVelocityX > 0 && _lookingLeft == true)
                {
                    Turn();
                }

                else if (_rigidbody.linearVelocityX < 0 && _lookingLeft == false)
                {
                    Turn();
                }

                SwitchPatrolPoints();
                break;
        }
        
        IgnorLayers();
        DirectionRaycast();
        if (_raycastHit = Physics2D.Raycast(_spawnBulletPoint.position, _rayDirection, _raycastMaxDistance, _ignorLayer))
        {
            _currentTimeToContinuePatrolling = _timeToContinuePatrolling;
            _isSeesThePlayer = true;
            _currentState = SHOOT_STATE;
        }

        else
        {
            _isSeesThePlayer = false;
            ContinuePatrolling();
            _currentTimeToContinuePatrolling -= Time.deltaTime;
        }
    }

    private void IgnorLayers()
    {
        _ignorLayer = _ignorLayerGround | _ignorLayerBullet | _ignorLayerEnemy | _ignorLayerEnemyGround | _ignorLayerPlayerGround | _ignorLayerHarmless | _ignorLayerEnemyBullet | _ignorCameraConfiner;
        _ignorLayer = ~_ignorLayer;
    }

    private void Shoot()//вызывается через Event в анимации
    {
        _currentBullet = Instantiate(_bullet, _spawnBulletPoint.position, Quaternion.identity);
        _currentBulletRigidbody = _currentBullet.GetComponent<Rigidbody2D>();
        _audioSource.clip = _sounds._currentSound[0];
        _audioSource.Play();

        if (gameObject.transform.localScale.x > 0)
        {
            _currentBulletRigidbody.linearVelocity = new Vector2(_speedBullet * 1, _currentBulletRigidbody.linearVelocity.y);
        }

        else if (gameObject.transform.localScale.x < 0)
        {
            _currentBulletRigidbody.linearVelocity = new Vector2(_speedBullet * -1, _currentBulletRigidbody.linearVelocity.y);
        }
    }

    private void ContinuePatrolling()
    {
        _currentTimeToContinuePatrolling -= Time.deltaTime;
        if (_currentTimeToContinuePatrolling <=0 && _currentTimeToContinuePatrolling >= -1)
        {
            _currentState = WALK_STATE;
        }
    }

    private void DirectionRaycast()
    {
        if (transform.localScale.x > 0)
        {
            _rayDirection = Vector2.right;
        }

        else if (transform.localScale.x < 0)
        {
            _rayDirection = Vector2.left;
        }
    }

    private void Turn()
    {
        _lookingLeft = !_lookingLeft;
        _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void SwitchPatrolPoints()
    {
        if (Vector2.Distance(_enemyPatrolPoints[_patrolPoint].position, transform.position) < 0.2f)
        {
            _currentState = IDLE_STATE;
            _enemyPatrolPoints[_patrolPoint].gameObject.SetActive(false);
            _patrolPoint++;
            if (_patrolPoint > _enemyPatrolPoints.Length - 1)
            {
                _patrolPoint = 0;
            }
            _enemyPatrolPoints[_patrolPoint].gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D _playerBullet)
    {
        if (_playerBullet.CompareTag("Bullet"))
        {
            TurnTowardsThePlayer();
        }
    }
    private void TurnTowardsThePlayer()//метод для поворота врага в сторону игрока при получении урона
    {
        _currentState = IDLE_STATE;
        _lookDirection = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        if (_lookDirection.x < 0 && _lookingLeft == false)
        {
            Turn();
        }

        else if (_lookDirection.x > 0 && _lookingLeft == true)
        {
            Turn();
        }
    }

    public void HurtBlinking()
    {
        _sprite.color = Color.black;
    }

    public void NormalSpriteColor()
    {
        _sprite.color = _normalSpriteColor;
    }
}

