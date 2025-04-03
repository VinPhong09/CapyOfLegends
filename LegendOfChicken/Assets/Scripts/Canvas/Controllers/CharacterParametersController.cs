using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParametersController : MonoBehaviour
{
    [SerializeField] private CharacterParameters[] CharacterParametersList;
    [SerializeField] private EnhanceElementParameters[] ChickenEnhElementsList0;
    [SerializeField] private EnhanceElementParameters[] ChickenEnhElementsList1;
    [SerializeField] private EnhanceElementParameters[] ChickenEnhElementsList2;
    [SerializeField] private EnhanceElementParameters[] ChickenEnhElementsList3;
    [SerializeField] private ChickenAttack chickenAttack0;
    [SerializeField] private ChickenAttack chickenAttack1;
    [SerializeField] private ChickenAttack chickenAttack2;
    [SerializeField] private ChickenAttack chickenAttack3;
    [SerializeField] private ChickenAttack chickenAttack4;

    public void RestartParameters(){
        foreach (CharacterParameters characterParameters in CharacterParametersList){
            characterParameters.Health = characterParameters.BaseHealth;
            characterParameters.Damage = characterParameters.BaseDamage;
            characterParameters.AttackSpeed = characterParameters.BaseAttackSpeed;
            characterParameters.CriticalHitChance = characterParameters.BaseCriticalHitChance;
            characterParameters.CriticalHitDamage = characterParameters.BaseCriticalHitDamage;
            characterParameters.HealthRecovery = characterParameters.BaseHealthRecovery;
            characterParameters.DoubleHitChance = characterParameters.BaseDoubleHitChance;
            characterParameters.TripleHitChance = characterParameters.BaseTripleHitChance;
        }
    }

    public void WriteToBase(){
        foreach (CharacterParameters characterParameters in CharacterParametersList){
            characterParameters.BaseHealth = characterParameters.Health;
            characterParameters.BaseDamage = characterParameters.Damage;
            characterParameters.BaseAttackSpeed = characterParameters.AttackSpeed;
            characterParameters.BaseCriticalHitChance = characterParameters.CriticalHitChance;
            characterParameters.BaseCriticalHitDamage = characterParameters.CriticalHitDamage;
            characterParameters.BaseHealthRecovery = characterParameters.HealthRecovery;
            characterParameters.BaseDoubleHitChance = characterParameters.DoubleHitChance;
            characterParameters.BaseTripleHitChance = characterParameters.TripleHitChance;
        }
    }

    private void Start(){
        RestartLevelOfParameters();
        RestartParameters();
        Invoke("ChickenDamageRefresh", 0.1f);
    }

    public void RestartLevelOfParameters(){
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList0){
            enhancedParameters.Level = 1;
            enhancedParameters.Cost = enhancedParameters.BaseCost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList1){
            enhancedParameters.Level = 1;
            enhancedParameters.Cost = enhancedParameters.BaseCost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList2){
            enhancedParameters.Level = 1;
            enhancedParameters.Cost = enhancedParameters.BaseCost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList3){
            enhancedParameters.Level = 1;
            enhancedParameters.Cost = enhancedParameters.BaseCost;
        }
    }

    public void WriteBaseCostFromCost(){
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList0){
            enhancedParameters._baseCost = enhancedParameters._cost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList1){
            enhancedParameters.BaseCost = enhancedParameters.Cost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList2){
            enhancedParameters.BaseCost = enhancedParameters.Cost;
        }
        foreach (EnhanceElementParameters enhancedParameters in ChickenEnhElementsList3){
            enhancedParameters.BaseCost = enhancedParameters.Cost;
        }
    }

    public void ChickenDamageRefresh(){
        chickenAttack0.DamageRefresh();
        chickenAttack1.DamageRefresh();
        chickenAttack2.DamageRefresh();
        chickenAttack3.DamageRefresh();
        // chickenAttack4.DamageRefresh();
    }

}
