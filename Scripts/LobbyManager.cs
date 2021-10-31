using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks // класс подключения к комнате
{
    public InputField createInput; // поле для ввода имени сервера при создании
    public InputField joinInput; // поле ввода имени сервера при подключении к созданному кем-то серверу
    void Start()
    {
        Time.timeScale = 1; // снимаем с паузы
    }
    public void RandomRoom() // подключение к случайной комнате
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void CreateRoom() // метод создания комнаты
    {
        RoomOptions roomOptions = new RoomOptions(); // создаем объект опции комнаты
        roomOptions.MaxPlayers = 2; // максимальное количество однвременных игроков в комнате
        PhotonNetwork.CreateRoom(createInput.text, roomOptions); // создание комнаты с именем что ввели в поле ввода
    }

    public void JoinRoom() // подключение к уже созданной комнате
    {
        PhotonNetwork.JoinRoom(joinInput.text); // подключаемся к серверу который уже был созддан кем-то, по имени
    }

    public override void OnJoinedRoom() // метод действий при подключения к комнате
    {
        PhotonNetwork.LoadLevel("SampleScene"); // запускаем сцену с игровым полем
    }

}
