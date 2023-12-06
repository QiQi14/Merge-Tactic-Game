using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public CharacterManager characterManager;
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
        grid = new Character[rows, columns * 2];
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
        for (int i = 0; i < 1; i++)
        {
            string type = "Type " + i;
            int level = Random.Range(1, 4);
            float hp = Random.Range(30, 100);
            float mana = Random.Range(20, 60);
            float def = Random.Range(10, 20);
            float attack = Random.Range(5, 15);

            Character newCharacter = characterManager.CreateCharacter(type, level, hp, mana, def, attack);
            Vector2Int position = FindEmptyCell();

            if (position.x != -1)
            {
                grid[position.x, position.y] = newCharacter;
                AddCharacterToCell(position.x, position.y, newCharacter);
            }
        }
    }

    void AddCharacterToCell(int row, int column, Character character)
    {
        float xPosition;

        if (column < columns)
        {
            xPosition = gridStartPoint.x + column * spacingX;
        } else
        {
            xPosition = - (gridStartPoint.x + (column - columns) * spacingX);
        }

        float yPosition = gridStartPoint.y - row * spacingY;

        Vector3 cellPosition = new Vector3(xPosition, yPosition, 0);
        Instantiate(characterManager.characterPrefab, cellPosition, Quaternion.identity);
    }

    Vector2Int FindEmptyCell()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns * 2; j++)
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

    public void OnAddCharacterClick()
    {
        AddInitialCharacters();
    }

    public void OnRemoveCharacterClick()
    {
        characterManager.ClearGrid();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns * 2; j++)
            {
                grid[i, j] = null; // Làm sạch lưới
            }
        }
    }
}
