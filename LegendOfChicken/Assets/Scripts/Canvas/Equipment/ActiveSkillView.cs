using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveSkillView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _selected;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _locked;
    [SerializeField] private Color _selectedColor;

    [field: SerializeField] public ActiveSkillsParameters ActiveSkillsParameters { get; set; }

    private void OnEnable()
    {
        _icon.sprite = ActiveSkillsParameters.Icon;

        CheckForLocked();
    }

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            _selected.gameObject.SetActive(true);
            ActiveSkillsParameters.IsSelected = true;
        }
        else
        {
            _selected.gameObject.SetActive(false);
            ActiveSkillsParameters.IsSelected = false;
        }
    }

    public void SetSelectedView(bool isSelected)
    {
        if (isSelected)
        {
            _frame.color = _selectedColor;
        }
        else
        {
            _frame.color = Color.white;
        }
    }

    public void CheckForLocked()
    {
        if (ActiveSkillsParameters.IsLocked)
        {
            _locked.gameObject.SetActive(true);
        }
        else
        {
            _locked.gameObject.SetActive(false);
        }
    }
}
