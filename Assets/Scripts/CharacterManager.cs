using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab;

    private List<Character> characters = new List<Character>();
    private List<Character> mergedCharacters = new List<Character>();

    public Character CreateCharacter(string type, int level, CharacterStats stats)
    {
        Debug.Log($"Character Created: Type={type}, Level={level}");
        Character newCharacter = new Character(type, level, stats);
        newCharacter.SetSkill(0, new PassiveSkill("Passive Ability"));
        newCharacter.SetSkill(1, new NormalAttackSkill("Normal Attack"));
        newCharacter.SetSkill(2, new UltimateAttackSkill("Ultimate Attack"));

        characters.Add(newCharacter);

        return newCharacter;
    }

    public void MergeCharacters()
    {
        Debug.Log($"Starting Merge. Character count: {characters.Count}");
        characters.Sort((c1, c2) =>
        {
            int typeComparison = c1.type.CompareTo(c2.type);
            if (typeComparison != 0) { return typeComparison; }
            return c1.level.CompareTo(c2.level);
        });

        for (int i = 0; i < characters.Count - 1; i++)
        {
            if (characters[i].type == characters[i + 1].type && characters[i].level == characters[i + 1].level)
            {
                characters[i].level++; // Tăng cấp độ của nhân vật thứ nhất

                Debug.Log($"Merged character at index {i} to level {characters[i].level}");
                characters.RemoveAt(i + 1); // Xóa nhân vật thứ hai
            }
            else
            {
                mergedCharacters.Add(characters[i]); // Thêm nhân vật không được merge vào mergedCharacters
                Debug.Log($"Characters at index {i} and {i + 1} do not match for merge");
            }
        }

        if (characters.Count > 0 && !mergedCharacters.Contains(characters[characters.Count - 1]))
        {
            mergedCharacters.Add(characters[characters.Count - 1]);
        }
        characters = new List<Character>(mergedCharacters); // Cập nhật danh sách nhân vật
    }

    private void Update()
    {
        foreach (Character character in characters)
        {
            character.RegenManaOverTime();
            character.CheckAndActiveSkill(); // Kiem tra va active skill khi mana 100%
        }
    }

    public void Attack(Character character)
    {
        character.StartAttack();
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
