using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoot : MonoBehaviour //класс для фиксирования камерыы по оси Х
{
    private float _initialXPosition; //поле для позиции камеры по оси Х

    private void Start()
    {
        _initialXPosition = transform.position.x; //передаем ссылку на позицию Х
    }

    private void Update()
    {
        var position = transform.position; 
        position.x = _initialXPosition; 
        transform.position = position;
    }
}
