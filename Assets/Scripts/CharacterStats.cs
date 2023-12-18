using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterStats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public float hp;
    public float mana;
    public float def;
    public float attack;
    public float manaRegenRate;

    public Sprite characterArtWork;
}
