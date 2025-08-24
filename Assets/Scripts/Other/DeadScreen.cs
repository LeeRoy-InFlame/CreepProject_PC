using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deadScreen;
    [SerializeField] private GameObject _interface;

    public void DeadScreenActive()
    {
        _deadScreen.SetActive(true);
        _interface.SetActive(false);
    }
}
