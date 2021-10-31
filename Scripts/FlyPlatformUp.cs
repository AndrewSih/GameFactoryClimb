
using UnityEngine;

public class FlyPlatformUp : MonoBehaviour //класс для движения платформы вверх вниз
{
    [SerializeField] private float distUp = 4f; //движение вверх [SerializeField] - значит что значения можно задавать с инспектора в юнити
    [SerializeField] private float distDown = 4f; // движение вниз 
    float dirX; //стартовое значение координат платформы
    float speed = 3f; // скорость движения платформы
    bool movingUp = true;
    void Start()
    {
        dirX = transform.position.y; //присваеваем координаты платформы стартовому значению, точка отсчета
    }
   
    void Update()
    {
        if (transform.position.y >= dirX + distUp) // если текущее значение координат больше либо равно 
        {                                                // значению стартовому + то что мы указали (distRight)
            movingUp = false; // двигаться вниз
        }
        else if (transform.position.y <= dirX - distDown) // если текущее значение меньше либо равно зн. стартовому - то что мы указали (distRight) 
        {
            movingUp = true;
        }
        if (movingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); // движение вверх, к текущей позитии прибавляем скорость 
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime); // движение вниз, к текущей позитии прибавляем скорость 
        }
    }
}