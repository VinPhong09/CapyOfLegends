using UnityEngine;

[CreateAssetMenu(fileName = "New Chicken Equipment", menuName = "ScriptableObjects/Equipment/Chicken")]
public class ChickenEquipmentParameters : ScriptableObject
{
    public ChickenParameters CharacterParameters;

    [Header("Top Section")]
    public WeaponEquipmentElementParameters WeaponIcon;
    public HatEquipmentElementParameters HatIcon;
    public ArmorEquipmentElementParameters ArmorIcon;
    public Sprite ExtraAttributeIcon;

    [Header("Middle Section")]
    [Header("Effects")]
    public Sprite EffectIcon1;
    public Sprite EffectIcon2;
    public Sprite EffectIcon3;
    public Sprite EffectIcon4;
    public Sprite EffectIcon5;

    [Header("Passive Skills")]
    public Sprite PassiveSkillsIcon;
    public string PassiveSkillsDescription;

    [Header("Active Skills")]
    public Sprite ActiveSkillIcon1;
    public Sprite ActiveSkillIcon2;
    public Sprite ActiveSkillIcon3;
}
