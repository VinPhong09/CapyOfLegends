using UnityEngine;
using DG.Tweening;

public class ChickenCardElement : MonoBehaviour
{
    [SerializeField] private Chicken _chicken;
    [SerializeField] private ChickenEquipment _chickenEquipment;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _locked;

    private ChickenEquipment _selectedChickenEquipment;

    public Chicken Chicken => _chicken;

    public ChickenEquipment ChickenEquipment => _chickenEquipment;

    private void OnEnable()
    {
        if (_chicken.ChickenParams.IsLocked)
        {
            _locked.gameObject.SetActive(true);
            return;
        }

        _locked.gameObject.SetActive(false);
    }

    public void UpSelectCard()
    {
        if (_chicken.ChickenParams.IsLocked)
            return;

        if (_selected.gameObject.activeInHierarchy)
            return;

        if (_buttons.activeInHierarchy)
        {
            ChickenCardController.Instance.HideAllButtonsOnCards();
        }
        else if (!_buttons.activeInHierarchy)
        {
            ChickenCardController.Instance.HideAllButtonsOnCards();
            _buttons.SetActive(true);
        }
    }

    public void OnSelectButton()
    {
        if (_chicken.ChickenParams.IsLocked)
            return;

        if (ChickenCardController.Instance == null)
            return;

        ChickenCardController.Instance.SelectedChickenCard = this;

        ChickenCardController.Instance.SelectCard(this);
    }

    public void HideButtons()
    {
        _buttons.SetActive(false);
    }

    public void OnActivateSelect()
    {
        _selected.SetActive(true);
    }

    public void HideSelect()
    {
        _selected.SetActive(false);
    }

    public void OpenUpgradeChickenPanel()
    {
        if (_selectedChickenEquipment != null)
            return;
        _selectedChickenEquipment = Instantiate(_chickenEquipment);
        _selectedChickenEquipment.Initialize();
    }
}
