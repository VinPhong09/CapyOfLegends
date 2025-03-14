using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentElementsController : MonoBehaviour
{
    private enum EquipmentElementsType
    {
        Weapon,
        Hat,
        Armor
    }

    [SerializeField] private EquipmentElementsType _equipmentElementsType;

    [SerializeField] private EquipmentElement[] _equipmentElements;

    [Header("UI Params")]
    [SerializeField] private GameObject _infoTopSection;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _frontFrame;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _progressBarTxt;
    [SerializeField] private GameObject _equippedImageObj;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private TextMeshProUGUI _ownedEffectTxt;
    [SerializeField] private TextMeshProUGUI _equippedEffectTxt;
    [SerializeField] private TextMeshProUGUI _profitText;

    public EquipmentElement SelectedElement { get; private set; }

    private ChickenEquipmentParameters _chickenEquipmentParameters;

    public void Initialize(ChickenEquipmentParameters chickenEquipmentParameters)
    {
        _chickenEquipmentParameters = chickenEquipmentParameters;

        UpdateCardsInfo();

        CheckForLocked();

        SelectedElement = GetFirstEquipmentElement();

        SetDisplayEquipmentElementInfo(SelectedElement);

        CheckForEquipped();
    }

    public void EquipElement()
    {
        if (SelectedElement == null || _chickenEquipmentParameters == null)
            return;

        switch (_equipmentElementsType)
        {
            case EquipmentElementsType.Weapon:

                if (_chickenEquipmentParameters.WeaponIcon != null)
                    _chickenEquipmentParameters.WeaponIcon.IsEquipped = false;

                _chickenEquipmentParameters.WeaponIcon = SelectedElement.EquipmentElementParameters as WeaponEquipmentElementParameters;
                _chickenEquipmentParameters.WeaponIcon.Icon = SelectedElement.EquipmentElementParameters.Icon;
                SelectedElement.IsEquipped = true;
                break;

            case EquipmentElementsType.Hat:
                if (_chickenEquipmentParameters.HatIcon != null)
                    _chickenEquipmentParameters.HatIcon.IsEquipped = false;

                _chickenEquipmentParameters.HatIcon = SelectedElement.EquipmentElementParameters as HatEquipmentElementParameters;
                _chickenEquipmentParameters.HatIcon.Icon = SelectedElement.EquipmentElementParameters.Icon;
                SelectedElement.IsEquipped = true;
                break;

            case EquipmentElementsType.Armor:
                if (_chickenEquipmentParameters.ArmorIcon != null)
                    _chickenEquipmentParameters.ArmorIcon.IsEquipped = false;

                _chickenEquipmentParameters.ArmorIcon = SelectedElement.EquipmentElementParameters as ArmorEquipmentElementParameters;
                _chickenEquipmentParameters.ArmorIcon.Icon = SelectedElement.EquipmentElementParameters.Icon;
                SelectedElement.IsEquipped = true;
                break;
        }

        CheckForEquipped();
        CheckForLocked();
        RenderViewController.Instance.Invoke();
    }

    public void CheckForEquipped()
    {
        if (SelectedElement == null)
            return;

        foreach (EquipmentElement i in _equipmentElements)
        {
            if (i.IsEquipped)
            {
                i.Selected.SetActive(true);
                continue;
            }
            i.Selected.SetActive(false);
        }

        if (SelectedElement.IsEquipped)
        {
            _equipButton.SetActive(false);
            _equippedImageObj.SetActive(true);
            return;
        }

        _equipButton.SetActive(true);
        _equippedImageObj.SetActive(false);
    }

    public EquipmentElement GetFirstEquipmentElement()
    {
        foreach(EquipmentElement i in _equipmentElements)
        {
            if (i.IsEquipped || !i.IsLocked)
                return i;
        }
        return null;
    }

    public void CheckForLocked()
    {
        foreach (EquipmentElement i in _equipmentElements)
        {
            if (i.IsLocked)
            {
                i.Locked.SetActive(true);
                continue;
            }
            i.Locked.SetActive(false);
        }
    }

    public void UpdateCardsInfo()
    {
        foreach (EquipmentElement i in _equipmentElements)
        {
            i.UpdateInfo();
        }
    }

    private void SetDisplayEquipmentElementInfo(EquipmentElement equipmentElement)
    {
        if(equipmentElement == null)
        {
            _infoTopSection.SetActive(false);
            return;
        }

        _infoTopSection.SetActive(true);
        _icon.sprite = equipmentElement.Icon.sprite;
        _frame.sprite = equipmentElement.Frame.sprite;
        _frontFrame.sprite = equipmentElement.FrontFrame.sprite;
        _frontFrame.color = equipmentElement.FrontFrame.color;
        _name.text = equipmentElement.EquipmentElementParameters.Label;
        _level.text = $"LV{SelectedElement.EquipmentElementParameters.Level}";
        _progressBar.minValue = 0;
        _progressBar.maxValue = SelectedElement.EquipmentElementParameters.MaxProgress;
        _progressBar.value = SelectedElement.EquipmentElementParameters.Progress;
        _progressBarTxt.text = $"{SelectedElement.EquipmentElementParameters.Progress}/{SelectedElement.EquipmentElementParameters.MaxProgress}";
        _profitText.text = $"+{SelectedElement.EquipmentElementParameters.Value}";

        switch (_equipmentElementsType)
        {
            case EquipmentElementsType.Weapon:
                _ownedEffectTxt.text = $"{_chickenEquipmentParameters.CharacterParameters.Damage}";
                _equippedEffectTxt.text = $"{SelectedElement.EquipmentElementParameters.Value + _chickenEquipmentParameters.CharacterParameters.Damage}";
                break;

            case EquipmentElementsType.Hat:
                _ownedEffectTxt.text = $"{_chickenEquipmentParameters.CharacterParameters.CriticalHitDamage}";
                _equippedEffectTxt.text = $"{SelectedElement.EquipmentElementParameters.Value + _chickenEquipmentParameters.CharacterParameters.CriticalHitDamage}";
                break;

            case EquipmentElementsType.Armor:
                _ownedEffectTxt.text = $"{_chickenEquipmentParameters.CharacterParameters.Health}";
                _equippedEffectTxt.text = $"{SelectedElement.EquipmentElementParameters.Value + _chickenEquipmentParameters.CharacterParameters.Health}";
                break;
        }
    }

    public void SelectCardToDisplay(EquipmentElement equipmentElement)
    {
        if (!equipmentElement.IsLocked)
        {
            SelectedElement = equipmentElement;
            SetDisplayEquipmentElementInfo(SelectedElement);
            CheckForEquipped();
            CheckForLocked();
            _infoTopSection.SetActive(true);
        }
    }

    public void DestroyThisContainerPanel(GameObject obj)
    {
        Destroy(obj);
    }
}
