using UnityEngine;

public class BackgroundSpaceship : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * _speed, transform.position.y);
    }
}
