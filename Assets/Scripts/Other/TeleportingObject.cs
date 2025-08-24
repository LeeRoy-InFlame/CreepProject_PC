using UnityEngine;

public class TeleportingObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _teleportationPoint;

    public void Teleportation()
    {
        _object.transform.position = _teleportationPoint.position;
    }
}
