using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector2 _lastCheckpointPosition = Vector2.zero;//Позиция последнего чекпоинта
    //Так как она статическая не будет сбрасываться до выключения игры

    private void Start()
    {
        if (_lastCheckpointPosition != Vector2.zero)
        {
            transform.position = _lastCheckpointPosition;
            //Будем телепортировать персонажа только если он "собрал" хотя бы один чекпоинт
        }
    }
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Checkpoint"))
        {
            _lastCheckpointPosition = _collision.transform.position;
            //Записываем позицию точки сохранения в переменную
        }
    }
}

