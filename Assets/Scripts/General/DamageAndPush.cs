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

    //���������� � ������ ��� ����������� ������� �������������� ������� _pushDirection � ������ �����������
    //����� _pushDirection = (_pushFrom - _objectPosition).normalized. ��, �� �����-�� ������� ��� �������
    //�� �� �������. �������� ����� Debug. _pushDirection �� ���������������� � 1, 1.
    //�����, �����. �������� ���. ������� �� �������, � ������� �������.
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
