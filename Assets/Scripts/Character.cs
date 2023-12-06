using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Character", menuName = "Unit Level 1")]

public interface ISkill
{
    void PerformSkill();
}
public abstract class SkillBase : ISkill
{
    protected string skillName;

    public SkillBase(string name)
    {
        skillName = name;
    }

    public abstract void PerformSkill();

    public string GetSkillName()
    {
        return skillName;
    }
}
public class PassiveSkill : SkillBase
{
    public PassiveSkill(string name) : base(name)
    {
        // thiet lap cho Passive Skill
    }

    public override void PerformSkill()
    {
        // logic de thuc hien Passive Skill
    }
}
public class NormalAttackSkill : SkillBase
{
    public NormalAttackSkill(string name) : base(name)
    {
        // thiet lap cho Normal Attack
    }

    public override void PerformSkill()
    {
        // logic de thuc hien Normal Attack
    }
}
public class UltimateAttackSkill : SkillBase
{
    public UltimateAttackSkill(string name) : base(name)
    {
        //thiet lap cho ultimate attack
    }

    public override void PerformSkill()
    {
        // logic de thuc hien Ultimate Attack
    }
}
public class Character
{

    public string type;
    public int level;
    public float hp;
    public float mana;
    public float def;
    public float attack;
    public float manaRegenRate;
    public ISkill[] skills;
    public ISkill activeSkill;


    public Character(string type, int level, float hp, float mana, float def, float attack) 
    {
        this.type = type;
        this.level = level;
        this.hp = hp;
        this.mana = mana;
        this.def = def; 
        this.attack = attack;
        this.manaRegenRate = 0.1f; // hoi 10% mana moi giay
        this.skills = new ISkill[3]; // mang 3 ky nang    
    }

    public void SetSkill(int index, ISkill skill)
    {
        if (index >= 0 && index < skills.Length)
        {
            skills[index] = skill;
        }
        else
        {
            Debug.LogError("Invalid skill index");
        }
    }

    public void UseSkill(int index)
    {
        if (index >= 0 && index < skills.Length && skills[index] != null)
        {
            skills[index].PerformSkill();
        }
        else
        {
            Debug.LogError("Invalid skill or skill not set!");
        }
    }

    public void StartAttack()
    {
        //logic tan cong


        // hoi mana khi tan cong
        RegenManaOnAttack();
    }

    public void RegenManaOnAttack()
    {
        mana += mana * 0.3f; // hoi mana tren moi don danh
        mana = Mathf.Min(mana, 100f); // mana toi da la 100
    }

    public void RegenManaOverTime()
    {
        mana += mana * manaRegenRate;
        mana = Mathf.Min(mana, 100f);
    }

    public void CheckAndActiveSkill()
    {
        if (mana >= 100f && activeSkill != null)
        {
            activeSkill.PerformSkill();
            mana = 0; // reset mana sau khi active ki nang
        }
    }
}
