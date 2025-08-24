using UnityEngine;

public class GUNController : MonoBehaviour
{
    [SerializeField] private GameObject _gunBullet;
    [SerializeField] private Transform _spawnGunBullet;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _raycastMaxDistance;

    private bool _iSeePlayer;
    private EnemyAnimationController _animation;
    private GameObject _currentGunBullet;
    private Vector2 _rayDirection;
    private RaycastHit2D _raycastHit;
    private Rigidbody2D _currentGunBulletRigidbody;

    #region ignorLayers
    private int _ignorLayer;
    private int _ignorLayerGround = 1 << 3;
    private int _ignorLayerBullet = 1 << 6;
    private int _ignorLayerEnemy = 1 << 8;
    private int _ignorLayerEnemyBullet = 1 << 12;
    private int _ignorCameraConfiner = 1 << 14;
    #endregion

    private void Start()
    {
        _animation = GetComponent<EnemyAnimationController>();
        _rayDirection = new Vector2(-1, 0);
    }

    private void FixedUpdate()
    {
        IgnorLayers();
        if (_raycastHit = Physics2D.Raycast(_spawnGunBullet.position, _rayDirection, _raycastMaxDistance, _ignorLayer))
        {
            _iSeePlayer = true;
        }

        else
        {
            _iSeePlayer = false;
        }

        _animation.EnemyShoot(_iSeePlayer);
    }

    private void IgnorLayers()
    {
        _ignorLayer = _ignorLayerGround | _ignorLayerBullet | _ignorLayerEnemy | _ignorLayerEnemyBullet | _ignorCameraConfiner;
        _ignorLayer = ~_ignorLayer;
    }

    public void Shoot()
    {
        _currentGunBullet = Instantiate(_gunBullet, _spawnGunBullet.position, Quaternion.identity);
        _currentGunBulletRigidbody = _currentGunBullet.GetComponent<Rigidbody2D>();
        _currentGunBulletRigidbody.linearVelocityX = _speedBullet;
    }
}
