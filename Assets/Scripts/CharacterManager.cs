using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab;

    private List<Character> characters = new List<Character>();
    private List<Character> mergedCharacters = new List<Character>();

    public Character CreateCharacter(string type, int level, float hp, float mana, float def, float attack)
    {
        Debug.Log($"Character Created: Type={type}, Level={level}, HP={hp}, Mana={mana}, Defense={def}, Attack={attack}");
        Character newCharacter = new Character(type, level, hp, mana, def, attack);
        newCharacter.SetSkill(0, new PassiveSkill("Passive Ability"));
        newCharacter.SetSkill(1, new NormalAttackSkill("Normal Attack"));
        newCharacter.SetSkill(2, new UltimateAttackSkill("Ultimate Attack"));
        return newCharacter;
    }

    /*public void MergeCharacters()
    {
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
                Character mergedCharacter = new Character(characters[i].type, characters[i].level + 1);
                mergedCharacters.Add(mergedCharacter);
                i++; // Bỏ qua nhân vật tiếp theo vì đã được merge
            }
        }
    }*/

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
