using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActiveSkillElement : MonoBehaviour
{
    [SerializeField] private RotateAnimation _rotateAnimation;
    [SerializeField] private Image _frame;
    [SerializeField] private Slider _timer;
    [SerializeField] private Image _defualtIcon;
    [SerializeField] private Image _icon;
    [SerializeField] private ActiveSkillsParameters _selectedActiveSkill;

    private Tween _tween;

    private bool _isProcessing = false;

    public ActiveSkillsParameters SelectedActiveSkill
    {
        get
        {
            return _selectedActiveSkill;
        }
        set
        {
            _selectedActiveSkill = value;
        }
    }

    private void Start()
    {
        CheckSelectedActiveSkill();
    }

    public void SetActiveSkill(ActiveSkillsParameters activeSkill)
    {
        _selectedActiveSkill = activeSkill;
    }
    
    public void OnProcessSkill()
    {
        if (_selectedActiveSkill == null || _isProcessing)
            return;

        _isProcessing = true;

        if (_selectedActiveSkill.ActiveSkillBehaviour != null)
        {
            ActiveSkill activeSkill = Instantiate(_selectedActiveSkill.ActiveSkillBehaviour);
            activeSkill.Initialize();
        }

        _timer.value = 0;
        _rotateAnimation.enabled = true;
        _tween = DOTween.Sequence()
            .Append(_timer.DOValue(1f, _selectedActiveSkill.ProcessDuraction))
            .AppendCallback(() => RecoverySkill());
    }

    private void RecoverySkill()
    {
        if (_selectedActiveSkill == null)
        {
            _rotateAnimation.enabled = false;
            _isProcessing = false;
            _timer.value = 0f;
            _frame.transform.eulerAngles = Vector3.zero;
            print("stopped");
            return;
        }

        _timer.value = 1f;
        _rotateAnimation.enabled = false;
        _tween = DOTween.Sequence()
            .Append(_timer.DOValue(0f, _selectedActiveSkill.RecoveryDuraction))
            .AppendCallback(() => {
                _isProcessing = false;
                if (ActiveSkillsController.Instance.IsAuto)
                    OnProcessSkill();
            });
    }

    public void CheckSelectedActiveSkill()
    {
        if(_selectedActiveSkill == null)
        {
            _icon.gameObject.SetActive(false);
            _defualtIcon.gameObject.SetActive(true);
        }
        else
        {
            _icon.gameObject.SetActive(true);
            _defualtIcon.gameObject.SetActive(false);
            _icon.sprite = _selectedActiveSkill.Icon;
        }
    }
}
