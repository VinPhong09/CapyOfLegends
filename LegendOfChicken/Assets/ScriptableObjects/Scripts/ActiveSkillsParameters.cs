using UnityEngine;

[CreateAssetMenu(fileName = "New Active Skill", menuName = "ScriptableObjects/Skills/Active")]
public class ActiveSkillsParameters : ScriptableObject
{
    public Sprite Icon;
    public float ProcessDuraction;
    public float RecoveryDuraction;
    public string Description;
    public bool IsLocked;
    public bool IsSelected;

    public ActiveSkill ActiveSkillBehaviour;
}
