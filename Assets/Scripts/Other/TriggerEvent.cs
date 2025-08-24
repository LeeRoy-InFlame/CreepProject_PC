using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private string[] _tags;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        foreach (var _tag in _tags)
        {
            if (_collision.CompareTag(_tag))
            {
                _event.Invoke();
            }
        }
    }
}
