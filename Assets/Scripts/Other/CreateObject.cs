using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Transform _createPoint;

    public void Create()
    {
        _gameObject = Instantiate(_gameObject, _createPoint.position, Quaternion.identity);
    }
}
