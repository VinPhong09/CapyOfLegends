using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingTreeElement : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _progressTxt;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _selectedImg;
    [SerializeField] private Image _completeImg;

    [SerializeField] private LevelingTreeElementParameters _levelingTreeElementParameters;

    public LevelingTreeElementParameters ElementParameters => _levelingTreeElementParameters;

    public Sprite Frame => _frame.sprite;
    public Image Selected => _selectedImg;

    public void Inititalize()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        _icon.sprite = _levelingTreeElementParameters.Icon;
        _progressTxt.text = $"({_levelingTreeElementParameters.Progress}/{_levelingTreeElementParameters.MaxProgress})";

        if (_levelingTreeElementParameters.IsComplete)
        {
            _completeImg.gameObject.SetActive(true);
            _progressTxt.color = Color.green;
        }
    }
}
