using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveSkillsController : MonoBehaviour
{
    public static ActiveSkillsController Instance;

    [SerializeField] private ActiveSkillElement[] _activeSkillsElements;
    [SerializeField] private List<ActiveSkillsParameters> _activeSkillsList;

    [Header("Auto Button")]
    [SerializeField] private TextMeshProUGUI _textAutoButton;
    [SerializeField] private RotateAnimation _animationAutoButton;

    public bool IsAuto { get; private set; }

    public void Initialize()
    {
        Instance = this;
    }

    private void Awake()
    {
        IsAuto = true;
        SetAutoButtonView(IsAuto);

        UpdateActiveSkills();
    }

    public void OnAutoButton()
    {
        IsAuto = !IsAuto;
        SetAutoButtonView(IsAuto);
    }

    private void SetAutoButtonView(bool toggle)
    {
        if (toggle)
        {
            _animationAutoButton.enabled = true;
            _textAutoButton.text = "AUTO ON";
            OnProcessingAllSkills();
        }
        else
        {
            _animationAutoButton.enabled = false;
            _textAutoButton.text = "AUTO OFF";
        }
    }

    public void UpdateActiveSkills()
    {
        int activeSkillElementIndex = 0;

        foreach (ActiveSkillElement i in _activeSkillsElements)
        {
            i.SelectedActiveSkill = null;
            i.CheckSelectedActiveSkill();
        }

        foreach (ActiveSkillsParameters i in _activeSkillsList)
        {
            if (i.IsSelected)
            {
                _activeSkillsElements[activeSkillElementIndex].SelectedActiveSkill = i;
                _activeSkillsElements[activeSkillElementIndex].CheckSelectedActiveSkill();
                activeSkillElementIndex++;
            }

            if (activeSkillElementIndex > _activeSkillsElements.Length)
                break;
        }

        if (IsAuto)
        {
            OnProcessingAllSkills();
        }
    }

    public void UpdateActiveSkillsList(params ChickenParameters[] chickenParameters)
    {
        _activeSkillsList.Clear();

        foreach (ChickenParameters chicken in chickenParameters)
        {
            if (chicken == null)
                continue;

            for (int i = 0; i < chicken.ActiveSkills.Length; i++)
            {
                _activeSkillsList.Add(chicken.ActiveSkills[i]);
            }
        }
    }

    private void OnProcessingAllSkills()
    {
        foreach (ActiveSkillElement skill in _activeSkillsElements)
        {
            skill.OnProcessSkill();
        }
    }
}
