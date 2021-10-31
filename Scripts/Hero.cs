using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hero : MonoBehaviour, IPunObservable 
{
    [SerializeField] private float speed; // ���� ��� �������� �������� �� �������� ������
    [SerializeField] private float jumpForce = 15f; // ���� ������ ������
    private bool isGrounded = false; // ���� ��� �������� �����
    private Rigidbody2D rb; // ������ �� ��������� rigidbody
    private Animator anim; // ������ �� ��������� ���������
    private SpriteRenderer sprite; // ������ �� ��������� �������
    public float normalSpeed = 4f; // ���� ��� ���������� �������� �������� �� ��������
    public GameObject canvas; // ���� ��� ������� ����������
    public GameObject winCan;// ��� ������� ����������
    public GameObject looseCan; // ��� ������� ������������
    public GameObject _camera; // ��� ������
    private PhotonView photonview; // ����� �� ��������� ������
    public static Hero Instance { get; set; } // ���� ���������� ������

    void Start()
    {
        Time.timeScale = 1; // ������ ������� ������� � �����
        speed = 0f; // ��������� �������� �������� ������
        anim = GetComponent<Animator>(); // �������� ���������� ���������
         photonview = GetComponent<PhotonView>(); // �������� ���������� ������ 
        AddObservable(); // ��������� ����� ������������������ ������� �� �������, ����������� ��� ������������� flipX
        if (!photonview.IsMine) // ���� �� �� ��������� �����
        {
            _camera.SetActive(false); // ��������� ������
            canvas.SetActive(false); // ��������� ������ ����������
            winCan.SetActive(false); // ��������� ������ ����������
            looseCan.SetActive(false); // ��������� ������ ������������
        }
    }
    private States State // �������� ��������� �������� ������
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>(); //�������� �������� rigidbody
        anim = GetComponent<Animator>(); //�������� ���������� ��������� 
        sprite = GetComponentInChildren<SpriteRenderer>(); //�������� �������� ������� ������
        Instance = this;
    }
    private void FixedUpdate()
    { 
        if (photonview.IsMine) // ���� �� ��������� �����
        {
        if (!isGrounded) State = States.jump; // ������� ���������� �������� ������
        if (isGrounded) State = States.idle; // �������� �����������
        if (speed != 0f) State = States.run; // �������� ����
        rb.velocity = new Vector2(speed, rb.velocity.y); // ������� �������� ���������
        }
   }
    void Update()
    {
        
    }
    public void PressPause() //����� ������� �� ������ �����
    {
        Camera.main.GetComponent<CanvasPause>().Pause(); // ��������������, ��������� ������, � � ��� �������� ����� 
    }
    public void LeftButtonDown() // ������ �������� ����� ��� ������� ������
    {
        if (speed >= 0f)  // ����  �������� ������ ��� ����� 0
        {
            speed = -normalSpeed; // ��������� ���������� �������� �������� � �������
            sprite.flipX = true; // ��������� ������ ( �����)
        }
    }
    public void RightButtonDown() // ����� �������� ������
    {
        if (speed <= 0f)
        {
            speed = normalSpeed; // ����������� �������� ���������� �������� 
            sprite.flipX = false; // �� ������������
           //transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void ButtonUp() // ����� ���� �������� ������
    {
        speed = 0f;
    }
    public void Jump() // ����� ������
    {
        if (isGrounded) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // ������ �������
    }

    private void OnCollisionEnter2D(Collision2D collision) // ����� ��� ��������������� � ����������� ��������
    {
        if (collision.gameObject.name.Equals("FlyPlatformRight")) // ���� �� ������� � ������  FlyPlatformRight
        {
            this.transform.parent = collision.transform; // ����� ����� �������� ������������ � ����������
        }
        if (collision.gameObject.name.Equals("FlyPlatformUp"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Ground") // ��������� ����� ��� �������
        {
            isGrounded = true; // �� �����
        }
    }
    private void OnCollisionExit2D(Collision2D collision) // ����� �� ����� �� ���������������
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
    private void OnTriggerEnter2D(Collider2D win) // ����� ��� ������, ��� ��������������� � ����������� ������
    {
        if (win.gameObject.tag == "Finish") // ���� ����� ����������� � ������� � ����� �����
        {
            if (photonview.IsMine) // ���� �� ��������� �����
            {
                winCan.SetActive(true); // �������� ������ ����������
                Time.timeScale = 0.01f; // ����������� ����� ����
            }
            else // ����  �� ��������� �����
            {
                looseCan.SetActive(true); // �������� ������ ������������
                Time.timeScale = 0.01f;
            }
        }
    }
   
    public void CanvasButtonExit() // ����� ������ ������ �� ����
    {
        Application.Quit();
    }
    public void CanvasButtonReset() // ����� ������ ����������� ����
    {
        GameObject.Find("SpawnPlayer").GetComponent<SpawnPlayer>().Leave(); 
    }

    private void AddObservable() //����� ������������������ ������� �� �������, ����������� ��� ������������� flipX
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
public enum States //enum ����� ��� ������������ ��������� �������� �����
{
    idle,
    run,
    jump
}
