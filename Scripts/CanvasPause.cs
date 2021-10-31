
using UnityEngine;

public class CanvasPause : MonoBehaviour //класс для вызова канваса паузы
{
    [SerializeField] private GameObject pauseCanvas; // объявляем поле для канваса и позволяем изменять прямо в юнити

    void Start()
    {
        pauseCanvas.SetActive(false); // выключаем канвас на старте
    }


    public void ExitButton() // метод выхода из приложения
    {
        Application.Quit(); // функция выхода
    }

    public void PlayButton() //метод возобновления игры по нажатию кнопки продолжить
    {
        pauseCanvas.SetActive(false); //выключает канвас паузы
        Time.timeScale = 1f; // возобновляет игру после паузы

    }
    public void Pause() //метод постановки игры на паузу
    {
        pauseCanvas.SetActive(true); // включает канвас паузы
        Time.timeScale = 0f; // останавливает время после нажатия на паузу
    }
}

