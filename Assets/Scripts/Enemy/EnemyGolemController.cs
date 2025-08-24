using UnityEngine;

public class EnemyGolemController : MonoBehaviour
{
    [SerializeField] private GameObject _particleFromUnderground;
    [SerializeField] private GameObject _explosion;

    private EnemyAnimationController _animation;
    private RaycastHit2D _raycast;
    private GameObject _currentparticleFromUnderground;
    private GameObject _currentExplosion;

    #region States
    private int _currentState;
    private const int IDLE_STATE = 0;
    private const int ATTACK_STATE = 1;
    #endregion

    //игнорирование слоев кроме "Ground"
    #region ignorLayers
    private int _ignorLayer;
    private int _ignorLayerIgnorRaycast = 1 << 2;
    private int _ignorLayerBullet = 1 << 6;
    private int _ignorLayerEnemy = 1 << 8;
    private int _ignorLayerHarmless = 1 << 9;
    private int _ignorLayerEnemyGround = 1 << 10;
    private int _ignorLayerPlayerGround = 1 << 11;
    private int _ignorLayerEnemyBullet = 1 << 12;
    private int _ignorLayerCameraConfiner = 1 << 14;
    #endregion

    private void Start()
    {
        _currentState = IDLE_STATE;
        _animation = GetComponent<EnemyAnimationController>();
    }
    //основное действие - атака. Происходит запуск анимации, когда игрок попадает в триггер.
    //далее через event в анимации запускается метод PreparingAttack затем ExplosionAttack.
    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case IDLE_STATE:
                _animation.Idle();
                break;

            case ATTACK_STATE:
                _animation.Attack();
                
                break;

        }

        IgnorLayers();
    }

    private void OnTriggerStay2D(Collider2D _collisionForAttack)
    {
        if (_collisionForAttack.CompareTag("PlayerBody"))
        {
            _currentState = ATTACK_STATE;
        }
        
    }

    private void OnTriggerExit2D(Collider2D _collisionForAttack)
    {
        if (_collisionForAttack.CompareTag("PlayerBody"))
        {
            _currentState = IDLE_STATE;
        }
            
    }

    private void IgnorLayers()
    {
        _ignorLayer = _ignorLayerIgnorRaycast | _ignorLayerBullet | _ignorLayerEnemy | _ignorLayerEnemyGround | _ignorLayerPlayerGround | _ignorLayerHarmless | _ignorLayerEnemyBullet | _ignorLayerCameraConfiner;
        _ignorLayer = ~_ignorLayer;
    }

    //метод создает ParticleSystem в точке соприкосновения луча и поверхности (Ground)
    private void PreparingAttack()
    {
        if (_raycast = Physics2D.Raycast(GameObject.FindGameObjectWithTag("Player").transform.position, Vector2.down, Mathf.Infinity, _ignorLayer))
        {
            _currentparticleFromUnderground = Instantiate(_particleFromUnderground, _raycast.point, Quaternion.identity);
        }
    }

    //метод создает взрыв с триггером для нанесения урона игроку в месте расположения ParticleSystem
    private void ExplosionAttack()
    {
        _currentExplosion = Instantiate(_explosion, _currentparticleFromUnderground.transform.position, Quaternion.identity);
    }


}
