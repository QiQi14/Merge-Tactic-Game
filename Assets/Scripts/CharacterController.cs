using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Charaa : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxMana = 100f;
    public float currentMana;
    public float defense = 10f;
    public float attack = 20f;
    public float manaRegenPerHit = 30f;
    public float manaRegenPerSecond = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = 0f;
    }

    void Update()
    {
        RegenManaOverTime();
    }

    public void NormalAttack(GameObject target)
    {
        ApplyDamage(target);
        RegenMana(manaRegenPerHit);
    }

    public void ActivateSkill()
    {
        if (currentMana >= maxMana)
        {
            CastSkill();
            currentMana -= maxMana;
        }
    }

    void ApplyDamage(GameObject target)
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Logic xử lý khi nhân vật nhận sát thương
    }

    void RegenMana(float amount)
    {
        currentMana = Mathf.Min(maxMana, currentMana + amount);
    }

    void RegenManaOverTime()
    {
        currentMana = Mathf.Min(maxMana, currentMana + manaRegenPerSecond * Time.deltaTime);
    }

    void CastSkill()
    {
        // Logic của active skill
    }
}
