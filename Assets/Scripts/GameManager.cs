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
            string type = "Type " + Random.Range(1,5);
            int level = Random.Range(1, 4);
            Debug.Log($"Creating character: {type}, Level: {level}");
            CharacterStats stats = ScriptableObject.CreateInstance<CharacterStats>();
            stats.hp = Random.Range(30, 100);
            stats.mana = Random.Range(20, 60);
            stats.def = Random.Range(10, 20);
            stats.attack = Random.Range(5, 15);
            stats.manaRegenRate = 0.1f;
            Debug.Log($"Stats - HP: {stats.hp}, Mana: {stats.mana}, Def: {stats.def}, Attack: {stats.attack}");
            Character newCharacter = characterManager.CreateCharacter(type, level, stats);
            Vector2Int position = FindEmptyCell();
            Debug.Log($"Character position: {position}");
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
        GameObject createdCharacter = Instantiate(characterManager.characterPrefab, cellPosition, Quaternion.identity);
        Debug.Log("Created character prefab at: " + cellPosition + ", active: " + createdCharacter.activeSelf);
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
