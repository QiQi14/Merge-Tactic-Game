using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int level;  
    public int baseHealth;
    public int baseMana;
    public int baseDefense;
    public int baseAttack;
    public float manaRegenPerHit = 0.3f;
    public float manaRegenPerSecond = 0.1f;
    public SkillData normalAttack;
    public SkillData passiveSkill;
    public SkillData activeSkill;
}
