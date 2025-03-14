using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChickensPlatformElement : MonoBehaviour
{
    [SerializeField] private RawImage _chickenImg;
    [SerializeField] private Button _removeButton;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private int _platformNumber;
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _levelTxt;
    [SerializeField] private SelectedChickensByPointsParameter _selectedChickensByPointsParameter;

    private ChickenCardElement _chickenCardElement;

    private Chicken _selectedChicken;

    public int PlatformNumber => _platformNumber;

    private void OnEnable()
    {
        UpdateChicken();
    }

    public void SetChicken()
    {
        if (ChickenCardController.Instance == null)
            return;

        if (ChickenCardController.Instance.SelectedChickenCard == null)
            return;

        ChickenCardElement chickenCardElement = ChickenCardController.Instance.SelectedChickenCard;

        if (_selectedChickensByPointsParameter.TryFindSelectedChickenByIndex(chickenCardElement.Chicken.ChickenParams.Index))
        {
            _selectedChickensByPointsParameter.SetChickenByIndex(_platformNumber, chickenCardElement.Chicken);
            UpdateChicken();
            ChickensController.Instance.CreateChicken();
        }
    }

    public void UpdateChicken()
    {
        Chicken chicken = _selectedChickensByPointsParameter.GetChickenByIndex(_platformNumber);

        if(chicken == null)
        {
            _chickenImg.gameObject.SetActive(false);
            _removeButton.gameObject.SetActive(false);
            _nameTxt.gameObject.SetActive(false);
            _levelTxt.gameObject.SetActive(false);
            return;
        }

        _nameTxt.text = chicken.ChickenParams.Name;
        _levelTxt.text = $"LV{chicken.ChickenParams.Level}";

        _chickenImg.texture = chicken.ChickenParams.ChickenRenderView;
        _chickenImg.gameObject.SetActive(true);
        _removeButton.gameObject.SetActive(true);
        _nameTxt.gameObject.SetActive(true);
        _levelTxt.gameObject.SetActive(true);

        _selectedChicken = chicken;
    }

    public void OpenChickenEquipmentPanel()
    {
        if (!_chickenImg.gameObject.activeInHierarchy)
            return;

        _chickenCardElement = ChickenCardController.Instance.FindCardByIndex(_selectedChickensByPointsParameter
            .GetChickenByIndex(_platformNumber).ChickenParams.Index);

        ChickenEquipment chickenEquipment = Instantiate(_chickenCardElement.ChickenEquipment);
        chickenEquipment.Initialize();
    }

    public void ActivateArrow()
    {
        DisactivateRemoveButton();
        _arrow.SetActive(true);
    }

    public void DisactivateArrow()
    {
        ActivateRemoveButton();
        _arrow.SetActive(false);
    }

    public void DisactivateRemoveButton()
    {
        _removeButton.gameObject.SetActive(false);
    }

    public void ActivateRemoveButton()
    {
        if (_selectedChicken == null)
            return;

        _removeButton.gameObject.SetActive(true);
    }

    public void RemoveChickenFromPlatform()
    {
        if (_selectedChicken == null)
            return;
        
        if(_selectedChickensByPointsParameter.GetAmountOfChickens() <= 1)
        {
            // amount is 1 and cant remove chicken
            return;
        }

        _chickenCardElement = null;
        _selectedChickensByPointsParameter.SetChickenByIndex(_platformNumber, null);
        UpdateChicken();
        ChickensController.Instance.CreateChicken();
    }
}
