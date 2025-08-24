using UnityEngine;

public class IsaacIsDead : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _deadSprite;
    private GameObject _canvas;

    private void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");

    }

    private void Update()
    {
        if (_health.IsAlive == false)
        {
            Instantiate(_deadSprite, transform.position, Quaternion.identity);
            _canvas.GetComponent<DeadScreen>().DeadScreenActive();
            Destroy(gameObject);
        }
    }
}
