using UnityEngine;

public class DamageFromEnemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _pushForce;

    //нанесение урона игроку с отталкиванием врагом
    private void OnTriggerEnter2D(Collider2D _player)
    {
        if (_player.CompareTag("PlayerBody"))
        {
            _player.gameObject.GetComponentInParent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
            _player.gameObject.GetComponent<Health>().TakeDamage(_damage);
            _player.gameObject.GetComponentInParent<DamageAndPush>().Push(transform.position, _pushForce);
        }
        
    }
}
