using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public int level;
    public int maxHealth;
    public int maxMana;
    public int defense;
    public int attack;
    public Sprite characterSprite;
    
}