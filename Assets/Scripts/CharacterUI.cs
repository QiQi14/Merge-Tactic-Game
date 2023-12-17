using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    // Các biến UI khác...

    public void UpdateUI(Character character)
    {
        nameText.text = character.characterName;
        levelText.text = "Level: " + character.level.ToString();
        // Cập nhật các thông tin khác...
    }
}
