using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    private Character characterData;
    public CharacterUI characterUI; // Gán prefab UI này trong Inspector

    // Các biến để lưu trữ các thuộc tính của nhân vật
    private int currentHealth;
    private int currentMana;
    private int attack;
    private int defense;

    public SpriteRenderer spriteRenderer; // Đảm bảo gắn SpriteRenderer trong Inspector

    // Phương thức Setup để cài đặt nhân vật dựa trên dữ liệu từ Character
    public void Setup(Character data)
    {
        characterData = data;

        // Cài đặt sprite
        if (spriteRenderer)
        {
            spriteRenderer.sprite = characterData.characterSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer not set on " + gameObject.name);
        }

        // Cài đặt các thuộc tính
        currentHealth = characterData.maxHealth;
        currentMana = characterData.maxMana;
        attack = characterData.attack;
        defense = characterData.defense;

        // Cập nhật UI
        if (characterUI != null)
        {
            characterUI.UpdateUI(characterData);
        }
    }

    // Property để truy cập characterData từ bên ngoài
    public Character CharacterData
    {
        get { return characterData; }
    }

    // Cập nhật lại nhân vật sau khi merge
    public void UpdateCharacterData(Character newData)
    {
        characterData = newData;

        // Cập nhật các thuộc tính sau khi merge
        currentHealth = characterData.maxHealth;
        currentMana = characterData.maxMana;
        attack = characterData.attack;
        defense = characterData.defense;

        // Cập nhật UI sau khi merge
        if (characterUI != null)
        {
            characterUI.UpdateUI(characterData);
        }
    }

    // Thêm các phương thức khác như tấn công, phòng thủ, hồi mana, v.v.


}
