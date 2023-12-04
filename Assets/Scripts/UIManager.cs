using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject characterPrefab; //Prefab de hien thi nhan vat
    public Transform[] slots; //mang cac Transform tuong ung voi 18 o
    public CharacterManager characterManager;

    public void AddRandomCharacterToSlot()
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                GameObject newCharacterObject = Instantiate(characterPrefab);
                newCharacterObject.transform.position = slot.position; // Đặt vị trí
                newCharacterObject.transform.SetParent(slot, false); // Gán cha
                newCharacterObject.name = "CharacterName"; // Đặt tên cho GameObject mới

                break;
            }
        }
    }
}
