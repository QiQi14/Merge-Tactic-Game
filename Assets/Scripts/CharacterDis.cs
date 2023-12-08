using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDis : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI attackText;

    public void SetCharacterStats(CharacterData characterData)
    {
        nameText.text = "Name: " + characterData.characterName;
        levelText.text = "Level: " + characterData.level.ToString();
        healthText.text = "Health: " + characterData.baseHealth.ToString();
        manaText.text = "Mana: " + characterData.baseMana.ToString();
        defenseText.text = "Defense: " + characterData.baseDefense.ToString();
        attackText.text = "Attack: " + characterData.baseAttack.ToString();
    }
}
