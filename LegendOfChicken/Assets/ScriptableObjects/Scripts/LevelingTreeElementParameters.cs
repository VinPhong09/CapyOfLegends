using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "ScriptableObjects/Leveling Tree Element")]
public class LevelingTreeElementParameters : ScriptableObject
{
    public Sprite Icon;
    public string Label;
    public string Description;
    public int Progress;
    public int MaxProgress;
    public int Cost;
    public bool IsComplete;
}
