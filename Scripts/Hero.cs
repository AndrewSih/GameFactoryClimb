using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hero : MonoBehaviour, IPunObservable 
{
    [SerializeField] private float speed; // поле для скорости движения по сторонам игрока
    [SerializeField] private float jumpForce = 15f; // сила прыжка игрока
    private bool isGrounded = false; // поле для проверки земли
    private Rigidbody2D rb; // ссылка на компонент rigidbody
    private Animator anim; // ссылка на компонент аниматора
    private SpriteRenderer sprite; // ссылка на компонент спрайта
    public float normalSpeed = 4f; // поле для нормальной скорости движения по сторонам
    public GameObject canvas; // поле для канваса управдения
    public GameObject winCan;// для канваса победителя
    public GameObject looseCan; // для канваса проигравшего
    public GameObject _camera; // для камеры
    private PhotonView photonview; // сылка на компонент фотона
    public static Hero Instance { get; set; } // поле экземпляра игрока

    void Start()
    {
        Time.timeScale = 1; // ставим течение времени в норму
        speed = 0f; // стартовая скорость движения игрока
        anim = GetComponent<Animator>(); // получаем компоненты аниматора
         photonview = GetComponent<PhotonView>(); // получаем компоненты фотона 
        AddObservable(); // запускаем метод последовательность событий во времени, использовал для синхронизации flipX
        if (!photonview.IsMine) // если мы не локальный игрок
        {
            _camera.SetActive(false); // отключаем камеру
            canvas.SetActive(false); // отключаем канвас управления
            winCan.SetActive(false); // отключаем канвас победителя
            looseCan.SetActive(false); // отключаем канвас проигравшего
        }
    }
    private States State // получаем состояния анимации игрока
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>(); //получаем компонет rigidbody
        anim = GetComponent<Animator>(); //получаем компоненты аниматора 
        sprite = GetComponentInChildren<SpriteRenderer>(); //получаем элементы спрайта игрока
        Instance = this;
    }
    private void FixedUpdate()
    { 
        if (photonview.IsMine) // если мы локальный игрок
        {
        if (!isGrounded) State = States.jump; // условия выполнения анимации прыжка
        if (isGrounded) State = States.idle; // анимации бездействия
        if (speed != 0f) State = States.run; // анимации бега
        rb.velocity = new Vector2(speed, rb.velocity.y); // функция движения персонажа
        }
   }
    void Update()
    {
        
    }
    public void PressPause() //метод нажатия на кнопку паузы
    {
        Camera.main.GetComponent<CanvasPause>().Pause(); // вызываетскрипт, компонент камеры, и в нем вызывает паузу 
    }
    public void LeftButtonDown() // методо движения влево при нажатии кнопки
    {
        if (speed >= 0f)  // если  скорость больше или равно 0
        {
            speed = -normalSpeed; // присвоить нормальное значение скорости с минусом
            sprite.flipX = true; // повернуть спрайт ( влево)
        }
    }
    public void RightButtonDown() // метод движения вправо
    {
        if (speed <= 0f)
        {
            speed = normalSpeed; // присваиваем скорости нормальную скорость 
            sprite.flipX = false; // не поворачиваем
           //transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void ButtonUp() // метод если отпутили кнопку
    {
        speed = 0f;
    }
    public void Jump() // метод прыжка
    {
        if (isGrounded) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // задаем импульс
    }

    private void OnCollisionEnter2D(Collision2D collision) // метод при соприкосновении с коллайдером объектов
    {
        if (collision.gameObject.name.Equals("FlyPlatformRight")) // если на объекте с именем  FlyPlatformRight
        {
            this.transform.parent = collision.transform; // чтобы игрок двигался одновременно с платформой
        }
        if (collision.gameObject.name.Equals("FlyPlatformUp"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Ground") // проверяем землю под игроком
        {
            isGrounded = true; // на земле
        }
    }
    private void OnCollisionExit2D(Collision2D collision) // метод на выход из соприкосновения
    {
        if (collision.gameObject.name.Equals("FlyPlatformRight"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.name.Equals("FlyPlatformUp"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D win) // метод для финиша, при соприкосновении с коллайдером финиша
    {
        if (win.gameObject.tag == "Finish") // если игрок прикоснулся к объекту с тэгом финиш
        {
            if (photonview.IsMine) // если мы локальный игрок
            {
                winCan.SetActive(true); // включить канвас победителя
                Time.timeScale = 0.01f; // затормозить время игры
            }
            else // если  не локальный игрок
            {
                looseCan.SetActive(true); // включить канвас проигравшего
                Time.timeScale = 0.01f;
            }
        }
    }
   
    public void CanvasButtonExit() // метод кнопки выхода из игры
    {
        Application.Quit();
    }
    public void CanvasButtonReset() // метод кнопки перезапуска игры
    {
        GameObject.Find("SpawnPlayer").GetComponent<SpawnPlayer>().Leave(); 
    }

    private void AddObservable() //метод последовательность событий во времени, использовал для синхронизации flipX
    {
        if (!photonview.ObservedComponents.Contains(this))
        {
            photonview.ObservedComponents.Add(this);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(sprite.flipX);
        }
        else
        {
            sprite.flipX = (bool)stream.ReceiveNext();
        }
    }
   
}
public enum States //enum класс для перечесдения состояний анимации героя
{
    idle,
    run,
    jump
}
