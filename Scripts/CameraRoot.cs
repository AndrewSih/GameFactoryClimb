using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoot : MonoBehaviour //����� ��� ������������ ������� �� ��� �
{
    private float _initialXPosition; //���� ��� ������� ������ �� ��� �

    private void Start()
    {
        _initialXPosition = transform.position.x; //�������� ������ �� ������� �
    }

    private void Update()
    {
        var position = transform.position; 
        position.x = _initialXPosition; 
        transform.position = position;
    }
}
