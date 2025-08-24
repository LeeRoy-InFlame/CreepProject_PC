using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    private int _spawnLimit;

    private GameObject _currentEnemy;
    private GameObject _player;
    private Transform _playerPosition;
    private int _currentNumberOfEnemies;
    private bool _spawnOn = false;
    private bool _firstStart = true;

    private void Start()
    {
        FindPlayer();
        _spawnLimit = 1;
    }

    private void Update()
    {
        if (_player != null)
        {
            if (Vector2.Distance(_playerPosition.position, gameObject.transform.position) < 1f && _firstStart == true)
            {
                _spawnOn = true;
                _firstStart = false;
            }
        }
        if (_spawnOn == true)
        {
            while (_currentNumberOfEnemies < _spawnLimit)
            {
                _currentEnemy = Instantiate(_enemies[_currentNumberOfEnemies], transform.position, Quaternion.identity);
                _currentNumberOfEnemies++;
                _spawnOn = false;
            }
        }

        if (_firstStart == false && _currentEnemy == null)
        {
            _spawnLimit++;
            _spawnOn = true;
        }

        if (_currentNumberOfEnemies == _enemies.Length)
        {
            Destroy(gameObject);
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
