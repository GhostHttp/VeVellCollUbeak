using UnityEngine;
using UnityEngine.UI;
using System;

public class MazeGenerator : MonoBehaviour
{
    [Header("Main Settings")]
    public int width = 30;
    public int height = 15;
    public float wallThickness = 8f;
    public Color wallColor = Color.black;

    [Header("References")]
    public RectTransform gameField;
    public RectTransform wallsContainer;
    public GameObject wallPrefab;
    public RectTransform startPoint;
    public RectTransform endPoint;

    private bool[,] visitedCells;
    private bool[,] horizontalWalls;
    private bool[,] verticalWalls;
    private Vector2 startPosition;
    private Vector2 endPosition;

    void Start()
    {
        GenerateMaze();
    }

    public void GenerateMaze()
    {
        // ������� ����������� ���������
        foreach (Transform child in wallsContainer)
            Destroy(child.gameObject);

        // ������������� ��������
        visitedCells = new bool[width, height];
        horizontalWalls = new bool[width, height + 1];
        verticalWalls = new bool[width + 1, height];

        // 1. ������� ��� ����� ����������
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                if (x < width) horizontalWalls[x, y] = true;
                if (y < height) verticalWalls[x, y] = true;
            }
        }

        // 2. �������� �������� ������� ��� ����� � ������
        System.Random rng = new System.Random();
        int startSide = rng.Next(4); // 0-����, 1-�����, 2-���, 3-����
        int endSide = (startSide + 2) % 4; // ��������������� �������

        // 3. ��������� ������/�������
        CreateRandomEntrance(startSide, true, rng);
        CreateRandomEntrance(endSide, false, rng);

        // 4. ��������� ��������
        int startX = rng.Next(width);
        int startY = rng.Next(height);
        RecursiveBacktracking(startX, startY, rng);

        // 5. ������������ ����
        float cellWidth = gameField.rect.width / width;
        float cellHeight = gameField.rect.height / height;

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                if (x < width && horizontalWalls[x, y])
                {
                    CreateWall(
                        new Vector2(x * cellWidth, -y * cellHeight + cellHeight / 2),
                        new Vector2(cellWidth, wallThickness)
                    );
                }
                if (y < height && verticalWalls[x, y])
                {
                    CreateWall(
                        new Vector2(x * cellWidth - cellWidth / 2, -y * cellHeight),
                        new Vector2(wallThickness, cellHeight)
                    );
                }
            }
        }

        // 6. ������������� �������
        startPoint.anchoredPosition = startPosition;
        endPoint.anchoredPosition = endPosition;
    }

    void CreateRandomEntrance(int side, bool isStart, System.Random rng)
    {
        float cellWidth = gameField.rect.width / width;
        float cellHeight = gameField.rect.height / height;

        switch (side)
        {
            case 0: // ������� �������
                int topX = rng.Next(width);
                horizontalWalls[topX, height] = false; // ������� �����
                if (isStart) startPosition = new Vector2(topX * cellWidth + cellWidth / 2, -height * cellHeight + cellHeight / 2);
                else endPosition = new Vector2(topX * cellWidth + cellWidth / 2, -height * cellHeight + cellHeight / 2);
                break;

            case 1: // ������ �������
                int rightY = rng.Next(height);
                verticalWalls[width, rightY] = false;
                if (isStart) startPosition = new Vector2(width * cellWidth - cellWidth / 2, -rightY * cellHeight - cellHeight / 2);
                else endPosition = new Vector2(width * cellWidth - cellWidth / 2, -rightY * cellHeight - cellHeight / 2);
                break;

            case 2: // ������ �������
                int bottomX = rng.Next(width);
                horizontalWalls[bottomX, 0] = false;
                if (isStart) startPosition = new Vector2(bottomX * cellWidth + cellWidth / 2, -0 + cellHeight / 2);
                else endPosition = new Vector2(bottomX * cellWidth + cellWidth / 2, -0 + cellHeight / 2);
                break;

            case 3: // ����� �������
                int leftY = rng.Next(height);
                verticalWalls[0, leftY] = false;
                if (isStart) startPosition = new Vector2(0 + cellWidth / 2, -leftY * cellHeight - cellHeight / 2);
                else endPosition = new Vector2(0 + cellWidth / 2, -leftY * cellHeight - cellHeight / 2);
                break;
        }
    }

    void RecursiveBacktracking(int x, int y, System.Random rng)
    {
        visitedCells[x, y] = true;

        // ��������� ������� �����������
        int[] directions = { 0, 1, 2, 3 };
        for (int i = 0; i < directions.Length; i++)
        {
            int r = rng.Next(i, directions.Length);
            (directions[r], directions[i]) = (directions[i], directions[r]);
        }

        foreach (int dir in directions)
        {
            int nx = x, ny = y;
            switch (dir)
            {
                case 0: ny = y + 1; break; // �����
                case 1: nx = x + 1; break; // ������
                case 2: ny = y - 1; break; // ����
                case 3: nx = x - 1; break; // �����
            }

            if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visitedCells[nx, ny])
            {
                // ������� ����� ����� ������� � ��������� �������
                if (dir == 0) horizontalWalls[x, y + 1] = false;
                else if (dir == 1) verticalWalls[x + 1, y] = false;
                else if (dir == 2) horizontalWalls[x, y] = false;
                else if (dir == 3) verticalWalls[x, y] = false;

                RecursiveBacktracking(nx, ny, rng);
            }
        }
    }

    void CreateWall(Vector2 position, Vector2 size)
    {
        GameObject wall = Instantiate(wallPrefab, wallsContainer);
        RectTransform rt = wall.GetComponent<RectTransform>();
        rt.anchoredPosition = position;
        rt.sizeDelta = size;
        wall.GetComponent<Image>().color = wallColor;
    }

    public Vector2 GetStartPosition() => startPosition;
    public Vector2 GetEndPosition() => endPosition;
}