using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public float checkRadius = 15f; // ������ �������� �������������
    public Color normalColor = new Color(0, 0.5f, 1f, 0.7f); // ���� �������
    public Color dangerColor = Color.red; // ���� ��� ������������

    [Header("References")]
    public Image cursorImage; // Image ��� �������
    public MazeGenerator mazeGenerator;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public TMP_Text resultText;

    private bool isActive = false;
    private bool gameEnded = false;
    private RectTransform gameField;
    private Vector2 lastValidPosition;

    void Start()
    {
        gameField = GetComponent<RectTransform>();
        Cursor.visible = false;
        cursorImage.gameObject.SetActive(false);
        lastValidPosition = mazeGenerator.GetStartPosition();
    }

    void Update()
    {
        if (!isActive || gameEnded) return;

        // �������� ������� ���� � ����������� ���������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gameField, Input.mousePosition, null, out Vector2 localPoint);

        // ��������� ����� �� �������
        if (IsOutsideMaze(localPoint))
        {
            GameOver("�� ����� �� ������� ���������!");
            return;
        }

        // ��������� ������������ �� �������
        if (CheckWallCollision(localPoint))
        {
            cursorImage.color = dangerColor;
            GameOver("�� ��������� �����!");
            return;
        }

        // ��������� ���������� ������
        if (Vector2.Distance(localPoint, mazeGenerator.GetEndPosition()) < checkRadius)
        {
            WinGame();
            return;
        }

        // ��������� ������� �������
        cursorImage.rectTransform.anchoredPosition = localPoint;
        lastValidPosition = localPoint;
        cursorImage.color = normalColor;
    }

    bool IsOutsideMaze(Vector2 position)
    {
        Rect mazeRect = new Rect(0, -gameField.rect.height, gameField.rect.width, gameField.rect.height);
        return !mazeRect.Contains(position);
    }

    bool CheckWallCollision(Vector2 position)
    {
        foreach (Transform wall in mazeGenerator.wallsContainer)
        {
            Rect wallRect = wall.GetComponent<RectTransform>().rect;
            Vector2 wallPos = wall.GetComponent<RectTransform>().anchoredPosition;

            wallRect.x = wallPos.x - wallRect.width / 2;
            wallRect.y = wallPos.y - wallRect.height / 2;

            if (wallRect.Overlaps(new Rect(position.x - checkRadius / 2,
                                     position.y - checkRadius / 2,
                                     checkRadius, checkRadius)))
            {
                return true;
            }
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isActive = true;
        cursorImage.gameObject.SetActive(true);
        cursorImage.rectTransform.anchoredPosition = lastValidPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        cursorImage.gameObject.SetActive(false);
    }

    void GameOver(string reason)
    {
        gameEnded = true;
        resultText.text = reason;
        gameOverPanel.SetActive(true);
        cursorImage.gameObject.SetActive(false);
    }

    void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        cursorImage.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        gameEnded = false;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        mazeGenerator.GenerateMaze();
        lastValidPosition = mazeGenerator.GetStartPosition();
    }
}