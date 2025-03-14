using UnityEngine;

public abstract class ChickenEquipmentElementParameters : ScriptableObject
{
    public string Label;
    public Sprite Icon;
    public int Value;
    public bool IsEquipped;
    public bool IsLocked;
    public int Level;
    public int Progress;
    public int MaxProgress;
    public IRarity.RarityType RarityType;
    public Buff[] Buffs;

    private readonly int[] MaxProgresses = new int[] { 5, 10, 20, 30, 40, 50 };

    public ChickenEquipmentElementParameters GetElementIfCanEnhance()
    {
        if (Progress >= MaxProgress)
            return this;

        return null;
    }

    public void CheckLevelToEnhance()
    {
        if (Progress < MaxProgress)
            return;

        while(Progress >= MaxProgress)
        {
            Progress -= MaxProgress;
            Level++;

            if (Level - 1 >= MaxProgresses.Length)
            {
                MaxProgress = MaxProgresses[^1];
                continue;
            }

            MaxProgress = MaxProgresses[Level - 1];
        }
    }
}
