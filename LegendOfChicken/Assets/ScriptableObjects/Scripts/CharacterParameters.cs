using UnityEngine;

public abstract class CharacterParameters : ScriptableObject
{
    public string Name;

    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float CriticalHitChance;
    public float CriticalHitDamage;
    public float HealthRecovery;
    public float DoubleHitChance;
    public float TripleHitChance;

    public float BaseHealth;
    public float BaseDamage;
    public float BaseAttackSpeed;
    public float BaseCriticalHitChance;
    public float BaseCriticalHitDamage;
    public float BaseHealthRecovery;
    public float BaseDoubleHitChance;
    public float BaseTripleHitChance;
    
}
