using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    [SerializeField] private float _raycastMaxDistance;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speedBullet;

    private RaycastHit2D _raycastHit;
    private Vector2 _rayDirection;
    private GameObject _currentBullet;
    private Rigidbody2D _currentBulletRigidbody;
    private bool _isShooting;
    private EnemyAnimationController _animation;

    private int _ignorLayer;
    private int _ignorLayerBullet = 1 << 6;
    private int _ignorLayerEnemy = 1 << 8;
    private int _ignorLayerEnemyStopper = 1 << 10;

    private void Start()
    {
        _isShooting = true;
        _animation = GetComponent<EnemyAnimationController>();
    }

    private void FixedUpdate()
    {
        Direction();
        IgnorLayers();
        if (_raycastHit = Physics2D.Raycast(_spawnBulletPoint.position, _rayDirection, _raycastMaxDistance, _ignorLayer))
        {
            _animation.Attack();
        }
    }

    private void Direction()
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

    public void Shoot()
    {
        if (_isShooting == true)
        {
            _currentBullet = Instantiate(_bullet, _spawnBulletPoint.position, Quaternion.identity);
            _currentBulletRigidbody = _currentBullet.GetComponent<Rigidbody2D>();

            if (gameObject.transform.localScale.x > 0)
            {
                _currentBulletRigidbody.linearVelocity = new Vector2(_speedBullet * 1, _currentBulletRigidbody.linearVelocity.y);
            }

            else if (gameObject.transform.localScale.x < 0)
            {
                _currentBulletRigidbody.linearVelocity = new Vector2(_speedBullet * -1, _currentBulletRigidbody.linearVelocity.y);
            }

            _isShooting = false;
        }
    }

    private void IgnorLayers()
    {
        _ignorLayer = _ignorLayerBullet | _ignorLayerEnemy | _ignorLayerEnemyStopper;
        _ignorLayer = ~_ignorLayer;
    }

}
