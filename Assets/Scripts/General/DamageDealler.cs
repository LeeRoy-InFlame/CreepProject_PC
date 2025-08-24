using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _pushForce;

    private Vector2 _pushFrom;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Enemy"))
        {
            BulletDamage(_collision);
        }

        else if (_collision.CompareTag("PlayerBody"))
        {
            EnemyDamage(_collision);
        }
    }

    private void BulletDamage(Collider2D _collision)
    {
        _collision.gameObject.GetComponentInParent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        _pushFrom = GameObject.FindGameObjectWithTag("Player").transform.position;
        _collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        _collision.gameObject.GetComponentInParent<DamageAndPush>().Push(_pushFrom, _pushForce);
        Destroy(gameObject);
    }

    private void EnemyDamage(Collider2D _collision)
    {
        _collision.gameObject.GetComponentInParent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        _collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        _collision.gameObject.GetComponentInParent<DamageAndPush>().Push(transform.position, _pushForce);


        //_pushFrom = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        //_pushFrom = gameObject.transform.position;
    }
}
