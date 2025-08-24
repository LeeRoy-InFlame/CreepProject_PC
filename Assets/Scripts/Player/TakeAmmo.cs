using Player.Inputs;
using UnityEngine;

public class TakeAmmo : MonoBehaviour
{
    [SerializeField] private int _ammoPoints;

    private void OnTriggerEnter2D(Collider2D _player)
    {
        if (_player.CompareTag("PlayerBody"))
        {
            _player.GetComponentInParent<PlayerShoot>().PickUpAmmo(_ammoPoints);
            Destroy(gameObject);
        }
    }
}
