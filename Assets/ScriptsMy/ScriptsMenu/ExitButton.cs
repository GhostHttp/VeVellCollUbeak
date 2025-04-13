using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button exitButton;

    private void Start()
    {
        // �������� ��������� Button
        exitButton = GetComponent<Button>();

        // ��������� ���������� �������
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ���������� Play Mode � ���������
#else
            Application.Quit(); // ������� ���������� � �����
#endif
    }
}
