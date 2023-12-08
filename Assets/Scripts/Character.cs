using UnityEngine;

// Lớp đại diện cho một nhân vật
[System.Serializable]
public class Character
{
    public string name;
    public int level;
    public int health;
    public int mana;
    public int defense;
    public int attack;
    public float manaRegenPerHit = 0.3f; 
    public float manaRegenPerSecond = 0.1f; 

    public SkillSet skills; 

    public Character(string name, int level)
    {
        this.name = name;
        this.level = level;
        
    }
}


[System.Serializable]
public class SkillSet
{
    public Skill normalAttack;
    public Skill passiveSkill;
    public Skill activeSkill;

    public SkillSet(Skill normalAttack, Skill passiveSkill, Skill activeSkill)
    {
        this.normalAttack = normalAttack;
        this.passiveSkill = passiveSkill;
        this.activeSkill = activeSkill;
    }
}


[System.Serializable]
public class Skill
{
    public string name;
    public int damage;

    public Skill(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }
}
