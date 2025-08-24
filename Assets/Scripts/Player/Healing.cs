using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private float _healthPoints;


    private void OnTriggerEnter2D(Collider2D _player)//подбор аптечки и получение лечения
    {
        if (_player.CompareTag("PlayerBody"))
        {
            _player.GetComponent<Health>().GetTreatment(_healthPoints);
            Destroy(gameObject);
        }
    }
}
