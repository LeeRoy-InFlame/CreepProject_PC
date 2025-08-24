using UnityEngine;
using UnityEngine.Events;

public class RunawayEnemies : MonoBehaviour
{
    [SerializeField] private float _minDistance;
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _event;
    [SerializeField] private GameObject _blood;
    private EnemyAnimationController _animationController;
    private GameObject _currentBlood;
    private Rigidbody2D _rigidbody;
    private GameObject _player;

    private void Start()
    {
        _animationController = GetComponent<EnemyAnimationController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        _animationController.Walk();
        if (_player == null || _player.activeInHierarchy == false)
        {
            _player = GameObject.FindWithTag("Player");
        }

        else if (Vector2.Distance(_player.transform.position, transform.position) <= _minDistance)
        {
            _rigidbody.linearVelocityX = _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D _player)
    {
        if (_player.CompareTag("PlayerBody"))
        {
            _currentBlood = Instantiate(_blood, transform.position, Quaternion.identity);
            _event.Invoke();
        }
    }
}
