using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMusicManager : MonoBehaviour
{
    public AudioSource[] mouseEffectMusic; // Массив звуковых эффектов для воспроизведения
    public InputAction mouseClickAction;

    void Awake()
    {
        // Создаем InputAction, если она не назначена в инспекторе
        if (mouseClickAction == null)
        {
            mouseClickAction = new InputAction(name: "MouseClick", binding: "<Mouse>/leftButton");
        }
    }

    void OnEnable()
    {
        mouseClickAction.Enable(); // Включаем InputAction
    }

    void OnDisable()
    {
        mouseClickAction.Disable(); // Выключаем InputAction
    }

    public void Update()
    {
        if (mouseClickAction.triggered) // Используем mouseClickAction.triggered
        {
            Debug.Log("Кнопка нажата!");
            StartEffectClicked();
        }
    }

    public void StartEffectClicked()
    {
        // Генерируем случайный номер для выбора музыкального эффекта
        int musicNumber = Random.Range(0, mouseEffectMusic.Length-1);

        if (mouseEffectMusic[musicNumber] != null && !mouseEffectMusic[musicNumber].isPlaying)
        {
            mouseEffectMusic[musicNumber].Play(); // Воспроизводим звук
        }
        else
        {
            Debug.Log("Ошибка: выбранный звук уже воспроизводится или не инициализирован."); // Логгируем ошибку
        }
    }
}

