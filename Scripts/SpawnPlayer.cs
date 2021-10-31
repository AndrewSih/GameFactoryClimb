using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviourPunCallbacks // ����� ��� ��������� ������� �� ������� ����

{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient) // ���� ����� ������ ������
        {
            Vector2 position1 = new Vector2(0.6f, -4.2f); // ������� ��������� ������� ������
            PhotonNetwork.Instantiate("Player1", position1, Quaternion.identity); // ��������� ������� ������ ( ��� , �������)
        }
        else // ���� ����� ������������ � ���������� �������
        {
            Vector2 position2 = new Vector2(0.62f, -4.2f); 
            PhotonNetwork.Instantiate("Player2", position2, Quaternion.identity);
        }
    }

    public void Leave() // ����� ��� ������ �� ������� 
    {
        SceneManager.LoadScene(1); // ��������� ����� 1
        PhotonNetwork.LeaveRoom(); // ����� �� ������� �� ������� ����������� � �����

    }
    public override void OnLeftRoom()
    {

        SceneManager.LoadScene(1);
    }
}