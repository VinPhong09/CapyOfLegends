using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingTreeController : MonoBehaviour
{
    [SerializeField] private LevelingTreeElement[] _levelingTreeElements;
    [SerializeField] private Image _iconImg;
    [SerializeField] private Image _frameImg;
    [SerializeField] private TextMeshProUGUI _progressTxt;
    [SerializeField] private TextMeshProUGUI _labelTxt;
    [SerializeField] private TextMeshProUGUI _descriptionTxt;
    [SerializeField] private TextMeshProUGUI _costTxt;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeElements();
    }

    public void DisplayElementInfo(LevelingTreeElement levelingTreeElement)
    {
        if (levelingTreeElement.ElementParameters.IsComplete)
            return;

        _iconImg.sprite = levelingTreeElement.ElementParameters.Icon;
        _frameImg.sprite = levelingTreeElement.Frame;
        _progressTxt.text = $"{levelingTreeElement.ElementParameters.Progress}/{levelingTreeElement.ElementParameters.MaxProgress})";
        _labelTxt.text = levelingTreeElement.ElementParameters.Label;
        _descriptionTxt.text = levelingTreeElement.ElementParameters.Description;
        _costTxt.text = levelingTreeElement.ElementParameters.Cost.ToString();

        DisableAllSelectedImg();
        levelingTreeElement.Selected.gameObject.SetActive(true);
    }

    public void UpdateAllElements()
    {
        foreach (LevelingTreeElement i in _levelingTreeElements)
        {
            i.UpdateData();
        }
    }

    private void InitializeElements()
    {
        foreach (LevelingTreeElement i in _levelingTreeElements)
        {
            i.Inititalize();
        }
    }

    private void DisableAllSelectedImg()
    {
        foreach (LevelingTreeElement i in _levelingTreeElements)
        {
            i.Selected.gameObject.SetActive(false);
        }
    }

}
