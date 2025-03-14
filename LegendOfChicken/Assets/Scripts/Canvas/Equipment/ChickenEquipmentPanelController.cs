using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChickenEquipmentPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform _selectedTab;

    [SerializeField] private RectTransform _chickenPanelButton;
    [SerializeField] private RectTransform _skillsPanelButton;

    [SerializeField] private ChickenEquipment _chickenEquipment;

    [Header("Active Skill")]
    [SerializeField] private TextMeshProUGUI _descrition;
    [SerializeField] private ActiveSkillView[] _activeSkills;
    [SerializeField] private ActiveSkillView _selectedActiveSkill;
    [SerializeField] private Image _selectedButton;
    [SerializeField] private Image _selectedImg;

    private readonly float MoveDuraction = 0.17f;
    private readonly float DeformationDuraction = 0.08f;

    private void OnEnable()
    {
        UpdateActiveSkills();
        SelectActiveSkill(_selectedActiveSkill);
        CheckForSelected();
        ResetSelectedViewActiveSkillsInfo();
    }

    public void Select(RectTransform targetPanel)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(_selectedTab.DOScaleX(0.8f, DeformationDuraction))
            .Append(_selectedTab.DOMove(targetPanel.transform.position, MoveDuraction))
            .Append(_selectedTab.DOScaleX(1f, DeformationDuraction));
    }

    public void OpenEquipmentContainerPanel(EquipmentElementsController equipmentElementsController)
    {
        EquipmentElementsController equipmentElements = Instantiate(equipmentElementsController);
        equipmentElements.Initialize(_chickenEquipment.ChickenEquipmentParameters);
    }

    private void UpdateActiveSkills()
    {
        for(int i = 0; i < _activeSkills.Length; i++)
        {
            _activeSkills[i].ActiveSkillsParameters = _chickenEquipment.ChickenEquipmentParameters.CharacterParameters.ActiveSkills[i];
        }
    }

    public void SelectActiveSkill(ActiveSkillView activeSkillView)
    {
        if (activeSkillView == null)
            return;

        ResetSelectedViewActiveSkillsInfo();

        _descrition.text = activeSkillView.ActiveSkillsParameters.Description;

        _selectedActiveSkill = activeSkillView;

        _selectedActiveSkill.SetSelectedView(true);

        _selectedButton.gameObject.SetActive(activeSkillView.ActiveSkillsParameters.IsSelected ? false : true);
        _selectedImg.gameObject.SetActive(activeSkillView.ActiveSkillsParameters.IsSelected ? true : false);
    }

    public void SetActiveActiveSkill()
    {
        if (_selectedActiveSkill == null)
            return;

        ResetSelectedActiveSkillsInfo();
        _selectedActiveSkill.SetSelected(true);

        ActiveSkillsController.Instance.UpdateActiveSkills();

        _selectedButton.gameObject.SetActive(_selectedActiveSkill.ActiveSkillsParameters.IsSelected ? false : true);
        _selectedImg.gameObject.SetActive(_selectedActiveSkill.ActiveSkillsParameters.IsSelected ? true : false);
    }

    public void CheckForSelected()
    {
        foreach (ActiveSkillView i in _activeSkills)
        {
            if (i.ActiveSkillsParameters.IsSelected)
            {
                i.SetSelected(true);
            }
            else
            {
                i.SetSelected(false);
            }
        }

        if (_selectedActiveSkill == null)
        {
            _selectedButton.gameObject.SetActive(false);
            _selectedImg.gameObject.SetActive(false);
        }
    }

    private void ResetSelectedActiveSkillsInfo()
    {
        foreach (ActiveSkillView i in _activeSkills)
        {
            i.SetSelected(false);
        }
    }

    private void ResetSelectedViewActiveSkillsInfo()
    {
        foreach (ActiveSkillView i in _activeSkills)
        {
            i.SetSelectedView(false);
        }
    }
}
