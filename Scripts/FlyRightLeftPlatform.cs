
using UnityEngine;

public class FlyRightLeftPlatform : MonoBehaviour //класс для движения платформы в стороны
{
    [SerializeField] private float distRight = 5f; //движение вправо [SerializeField] - значит что значения можно задавать с инспектора в юнити
    [SerializeField] private float distLeft = 5f; // движение влево 
    float dirX; //стартовое значение координат платформы
    float speed = 3f; // скорость движения платформы
    bool movingRight = true; 
    void Start()
    {
        dirX = transform.position.x; //присваеваем координаты платформы стартовому значению, точка отсчета
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= dirX + distRight) // если текущее значение координат больше либо равно 
        {                                              // значению стартовому + то что мы указали (distRight)
            movingRight = false; // двигаться влево
        }
        else if (transform.position.x <= dirX - distLeft)// если текущее значение меньше либо равно зн. стартовому - то что мы указали (distRight) 
        {
            movingRight = true; // двигаться вправо
        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); // движение вправо, к текущей позитии прибавляем скорость 
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); // движение влево
        }
    }
}
