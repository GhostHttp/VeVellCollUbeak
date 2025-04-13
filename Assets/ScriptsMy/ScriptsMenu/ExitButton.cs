using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button exitButton;

    private void Start()
    {
        // Получаем компонент Button
        exitButton = GetComponent<Button>();

        // Добавляем обработчик нажатия
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Остановить Play Mode в редакторе
#else
            Application.Quit(); // Закрыть приложение в билде
#endif
    }
}
