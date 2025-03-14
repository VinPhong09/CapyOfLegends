using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EnhancedElement : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frontFrame;
    [SerializeField] private Image _frame;
    [SerializeField] private TextMeshProUGUI _prevLvlTxt;
    [SerializeField] private TextMeshProUGUI _nextLvlTxt;
    [SerializeField] private RarityParameters _rarityParameters;

    private ChickenEquipmentElementParameters _chickenEquipmentElement;

    private readonly float ScaleDuraction = 0.25f;

    public void Initialize(ChickenEquipmentElementParameters chickenEquipmentElementParameters)
    {
        _chickenEquipmentElement = chickenEquipmentElementParameters;
        _rarityParameters.SetCardViewByRarity(ref _frame, ref _frontFrame, _chickenEquipmentElement.RarityType);
        _icon.sprite = _chickenEquipmentElement.Icon;

        _prevLvlTxt.text = (chickenEquipmentElementParameters.Level - 1).ToString();
        _nextLvlTxt.text = chickenEquipmentElementParameters.Level.ToString();

        transform.localScale = new Vector2(0f, 0f);

        transform.DOScale(1f, ScaleDuraction);
    }
}
