using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviourPunCallbacks // класс для появления игроков на игровом поле

{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient) // если игрок создал сервер
        {
            Vector2 position1 = new Vector2(0.6f, -4.2f); // позития появления первого игрока
            PhotonNetwork.Instantiate("Player1", position1, Quaternion.identity); // появление первого игрока ( имя , позития)
        }
        else // если игрок подключается к созданному серверу
        {
            Vector2 position2 = new Vector2(0.62f, -4.2f); 
            PhotonNetwork.Instantiate("Player2", position2, Quaternion.identity);
        }
    }

    public void Leave() // метод для выхода из комнаты 
    {
        SceneManager.LoadScene(1); // загрузить сцену 1
        PhotonNetwork.LeaveRoom(); // выйти из комнаты не покидая подключение к лобби

    }
    public override void OnLeftRoom()
    {

        SceneManager.LoadScene(1);
    }
}