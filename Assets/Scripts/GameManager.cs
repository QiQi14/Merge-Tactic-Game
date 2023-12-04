using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject characterPrefab;

    private Character[,] grid;

    int rows = 6;    // Số hàng
    int columns = 3; // Số cột
    float spacingX = 1.5f; // Khoảng cách giữa các ô theo trục x
    float spacingY = 1.5f; // Khoảng cách giữa các ô theo trục y
    Vector3 gridStartPoint = new Vector3(-3.8f, 3.5f, 0); // Điểm bắt đầu của lưới

    private void Start()
    {
        InitializeGrid();
        //AddInitialCharacters();
    }

    public void InitializeGrid()
    {
        grid = new Character[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = null;
                float xPosition = gridStartPoint.x + j * spacingX;
                float yPosition = gridStartPoint.y - i * spacingY;

                Instantiate(cellPrefab, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                Instantiate(cellPrefab, new Vector3(-xPosition, yPosition, 0), Quaternion.identity);
            }
        }
    }

    public void AddInitialCharacters()
    {
        for (int i = 0; i < 5; i++)
        {
            int level = Random.Range(1, 4);
            string type = "Type " + i;

            Character newCharacter = new Character(type, level);
            Vector2Int position = FindEmptyCell();

            if (position.x != -1)
            {
                grid[position.x, position.y] = newCharacter;
                AddCharacterToCell(position.x, position.y);
            }
        }
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = null; // Làm sạch lưới
            }
        }
    }

    void AddCharacterToCell(int row, int column)
    {
        Vector2 cellPosition = new Vector2(row, column);
        Instantiate(characterPrefab, cellPosition, Quaternion.identity);
    }

    Vector2Int FindEmptyCell()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (grid[i, j] == null)
                {
                    emptyCells.Add(new Vector2Int(i, j));
                }
            }
        }

        if (emptyCells.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyCells.Count);
            return emptyCells[randomIndex];
        }

        return new Vector2Int(-1, 1); // Khong tim thay o trong
    }

    public void OnButtonClick()
    {
        ClearGrid();
        AddInitialCharacters();
    }
}
