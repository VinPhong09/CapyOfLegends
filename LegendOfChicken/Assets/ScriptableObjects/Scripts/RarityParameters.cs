using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Rarity", menuName = "ScriptableObjects/Rarity")]
public class RarityParameters : ScriptableObject
{
    [Header("Common")]
    public Sprite CommonFrameImg;
    public Color CommonColor;

    [Header("Rare")]
    public Sprite RareFrameImg;
    public Color RareColor;

    [Header("Epic")]
    public Sprite EpicFrameImg;
    public Color EpicColor;

    [Header("Mythical")]
    public Sprite MythicalFrameImg;
    public Color MythicalColor;

    [Header("Legendary")]
    public Sprite LegendaryFrameImg;
    public Color LegendaryColor;

    public void SetCardViewByRarity(ref Image img, ref Image color, in IRarity.RarityType rarityType)
    {
        switch (rarityType)
        {
            case IRarity.RarityType.Common:
                img.sprite = CommonFrameImg;
                color.color = CommonColor;
                break;

            case IRarity.RarityType.Rare:
                img.sprite = RareFrameImg;
                color.color = RareColor;
                break;

            case IRarity.RarityType.Epic:
                img.sprite = EpicFrameImg;
                color.color = EpicColor;
                break;

            case IRarity.RarityType.Mythical:
                img.sprite = MythicalFrameImg;
                color.color = MythicalColor;
                break;

            case IRarity.RarityType.Legendary:
                img.sprite = LegendaryFrameImg;
                color.color = LegendaryColor;
                break;
        }
    }
}
