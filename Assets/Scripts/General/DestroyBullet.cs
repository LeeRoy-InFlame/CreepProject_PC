using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private float _maxTimeLifeOfBullet;
    private float _currentTimeLifeOfBullet;

    private void Start()
    {
        _currentTimeLifeOfBullet = _maxTimeLifeOfBullet;
    }

    private void Update()
    {
        _currentTimeLifeOfBullet -= Time.deltaTime;
        if (_currentTimeLifeOfBullet <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Destroy(gameObject);
    }
}
