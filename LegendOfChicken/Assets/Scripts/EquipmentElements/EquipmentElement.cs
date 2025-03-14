using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentElement : MonoBehaviour
{
    [SerializeField] private ChickenEquipmentElementParameters _chickenEquipmentElementParameters;

    [SerializeField] private RarityParameters _rarityParameters;

    [SerializeField] private EquipmentElementsController _controller;

    [SerializeField] public Image Frame;
    [SerializeField] public Image FrontFrame;

    [field: SerializeField] public Image Icon { get; private set; }
    [field: SerializeField] public TextMeshProUGUI LevelTxt { get; private set; }
    [field: SerializeField] public Slider ProgressBar { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ProgressBarTxt { get; private set; }
    [field: SerializeField] public GameObject EnhanceIcon { get; private set; }
    [field: SerializeField] public GameObject Selected { get; private set; }
    [field: SerializeField] public GameObject Locked { get; private set; }

    public bool IsEquipped {
        get
        {
            return _chickenEquipmentElementParameters.IsEquipped;
        }
        set
        {
            _chickenEquipmentElementParameters.IsEquipped = value;
        }
    }

    public bool IsLocked
    {
        get
        {
            return _chickenEquipmentElementParameters.IsLocked;
        }
        set
        {
            _chickenEquipmentElementParameters.IsLocked = value;
        }
    }

    public ChickenEquipmentElementParameters EquipmentElementParameters => _chickenEquipmentElementParameters;

    public void UpdateInfo()
    {
        CheckForEnhance();

        LevelTxt.text = $"Lv.{_chickenEquipmentElementParameters.Level}";

        ProgressBar.minValue = 0;
        ProgressBar.maxValue = _chickenEquipmentElementParameters.MaxProgress;
        ProgressBar.value = _chickenEquipmentElementParameters.Progress;

        ProgressBarTxt.text = $"{_chickenEquipmentElementParameters.Progress}/{_chickenEquipmentElementParameters.MaxProgress}";

        _rarityParameters.SetCardViewByRarity(ref Frame, ref FrontFrame, _chickenEquipmentElementParameters.RarityType);
    }

    public void CheckForEnhance()
    {
        //_chickenEquipmentElementParameters.CheckLevelToEnhance();

        if (_chickenEquipmentElementParameters.Progress >= _chickenEquipmentElementParameters.MaxProgress)
        {
            EnhanceIcon.gameObject.SetActive(true);
            ProgressBarTxt.color = Color.green;
        }
        else
        {
            EnhanceIcon.gameObject.SetActive(false);
            ProgressBarTxt.color = Color.white;
        }
    }

    public void SetSelectedElement()
    {
        _controller.SelectCardToDisplay(this);
    }

}
