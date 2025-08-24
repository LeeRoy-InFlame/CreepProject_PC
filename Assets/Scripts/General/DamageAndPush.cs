using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndPush : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _pushDirection;
    private Vector2 _objectPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    //изначально в методе был использован принцип преобразование вектора _pushDirection в вектор направления
    //через _pushDirection = (_pushFrom - _objectPosition).normalized. Но, по какой-то неясной мне причине
    //он не работал. Проверял через Debug. _pushDirection не преобразовывался в 1, 1.
    //Бился, устал. Придумал так. Надеюсь не костыль, а изящное решение.
    public void Push(Vector2 _pushFrom, float _pushForce)
    {
        _objectPosition = transform.position;
        _pushDirection = _pushFrom - _objectPosition;
        if (_pushDirection.x < 0)
        {
            _pushDirection.x = 1;
        }

        else if (_pushDirection.x > 0)
        {
            _pushDirection.x = -1;
        }
        _rigidbody.AddForce(_pushDirection * _pushForce);
    }
}
