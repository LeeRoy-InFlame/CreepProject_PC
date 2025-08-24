using UnityEngine;

public class DamageFromPlayer : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _pushForce;

    private Vector2 _pushFrom;

    private void OnTriggerEnter2D(Collider2D _enemy)
    {
        if (_enemy.CompareTag("Enemy"))
        {
            _pushFrom = GameObject.FindGameObjectWithTag("Player").transform.position;
            _enemy.gameObject.GetComponent<Health>().TakeDamage(_damage);
            _enemy.gameObject.GetComponentInParent<DamageAndPush>().Push(_pushFrom, _pushForce);
        }
    }
}
