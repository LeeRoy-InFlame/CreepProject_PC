using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsForDestruction;
    public void Destroy()
    {
        for (int i = 0; i < _objectsForDestruction.Length; i++)
        {
            Destroy(_objectsForDestruction[i]);
        }
    }
}
