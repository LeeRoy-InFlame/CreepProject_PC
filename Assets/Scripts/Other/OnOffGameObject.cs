using UnityEngine;

public class OnOffGameObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;

    public void OnOffObject()
    {
        for (int i = 0; i < _gameObjects.Length; i++)
        {
            if (!_gameObjects[i].activeInHierarchy)
            {
                _gameObjects[i].SetActive(true);
            }

            else
            {
                _gameObjects[i].SetActive(false);
            }
        }
    }
}
