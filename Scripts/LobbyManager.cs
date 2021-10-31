using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks // ����� ����������� � �������
{
    public InputField createInput; // ���� ��� ����� ����� ������� ��� ��������
    public InputField joinInput; // ���� ����� ����� ������� ��� ����������� � ���������� ���-�� �������
    void Start()
    {
        Time.timeScale = 1; // ������� � �����
    }
    public void RandomRoom() // ����������� � ��������� �������
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void CreateRoom() // ����� �������� �������
    {
        RoomOptions roomOptions = new RoomOptions(); // ������� ������ ����� �������
        roomOptions.MaxPlayers = 2; // ������������ ���������� ������������ ������� � �������
        PhotonNetwork.CreateRoom(createInput.text, roomOptions); // �������� ������� � ������ ��� ����� � ���� �����
    }

    public void JoinRoom() // ����������� � ��� ��������� �������
    {
        PhotonNetwork.JoinRoom(joinInput.text); // ������������ � ������� ������� ��� ��� ������� ���-��, �� �����
    }

    public override void OnJoinedRoom() // ����� �������� ��� ����������� � �������
    {
        PhotonNetwork.LoadLevel("SampleScene"); // ��������� ����� � ������� �����
    }

}
