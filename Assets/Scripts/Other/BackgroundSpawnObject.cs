using Unity.Cinemachine;
using UnityEngine;

public class BackgroundSpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;

    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _speed;
    [SerializeField] private CinemachineCamera _camera;
    private float _spawnTime;

    private GameObject _currentSpawnObject;

    private void Start()
    {
        _spawnTime = Random.Range(_minTime, _maxTime);
    }

    private void FixedUpdate()
    {
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0)
        {
            Destroy(_currentSpawnObject);
            _currentSpawnObject = Instantiate(_spawnObject, transform.position, Quaternion.identity);
            _spawnTime = Random.Range(_minTime, _maxTime);
        }

        if (_currentSpawnObject != null)
        {
            _currentSpawnObject.transform.position = new Vector2(_currentSpawnObject.transform.position.x + Time.deltaTime * _speed, transform.position.y);
            if (_speed > 0)
            {
                _currentSpawnObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
