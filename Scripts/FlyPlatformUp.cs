
using UnityEngine;

public class FlyPlatformUp : MonoBehaviour //����� ��� �������� ��������� ����� ����
{
    [SerializeField] private float distUp = 4f; //�������� ����� [SerializeField] - ������ ��� �������� ����� �������� � ���������� � �����
    [SerializeField] private float distDown = 4f; // �������� ���� 
    float dirX; //��������� �������� ��������� ���������
    float speed = 3f; // �������� �������� ���������
    bool movingUp = true;
    void Start()
    {
        dirX = transform.position.y; //����������� ���������� ��������� ���������� ��������, ����� �������
    }
   
    void Update()
    {
        if (transform.position.y >= dirX + distUp) // ���� ������� �������� ��������� ������ ���� ����� 
        {                                                // �������� ���������� + �� ��� �� ������� (distRight)
            movingUp = false; // ��������� ����
        }
        else if (transform.position.y <= dirX - distDown) // ���� ������� �������� ������ ���� ����� ��. ���������� - �� ��� �� ������� (distRight) 
        {
            movingUp = true;
        }
        if (movingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime); // �������� �����, � ������� ������� ���������� �������� 
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime); // �������� ����, � ������� ������� ���������� �������� 
        }
    }
}