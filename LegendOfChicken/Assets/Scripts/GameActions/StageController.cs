using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class StageController : MonoBehaviour
{
    public static StageController Instance;

    [SerializeField] private UnityEvent SubstageCompleted;
    [SerializeField] private UnityEvent SubstageTransitionCompleted;
    [SerializeField] private TextMeshProUGUI _stageText;
    [SerializeField] private Slider _stageSlider;
    [SerializeField] private TransitionBetweenLevels _transitionBetweenLevels;

    private int _stage;
    private int _subStage;

    private int _fightNumber;

    public readonly int MaxAmountOfFightsPerSubStage = 5;
    public readonly int MaxAmountOfSubStagesPerStage = 10;

    private readonly float[] SliderStageValues = {0f, 0.22f, 0.445f, 0.67f, 1f};

    public int FightNumber => _fightNumber;

    [field: SerializeField] public bool FarmMode { get; set; } = false;

    public void Initialize()
    {
        Instance = this;

        _stage = 1;
        _subStage = 1;

        _fightNumber = 1;
    }

    public void StageComplete()
    {
        if (!FarmMode)
            _stageSlider.gameObject.SetActive(true);

        if (_fightNumber >= MaxAmountOfFightsPerSubStage)
        {
            if (!FarmMode)
            {
                TransitionBetweenLevels transitionBetweenLevels = Instantiate(_transitionBetweenLevels);
                transitionBetweenLevels.Initialize(() => SubstageTransitionCompleted?.Invoke());
                SubstageCompleted?.Invoke();
            }
            return;
        }

        _fightNumber++;
        _stageSlider.DOValue(SliderStageValues[_fightNumber - 1], 0.5f).SetEase(Ease.Linear);
    }

    private void UpdateText()
    {
        _stageText.text = $"FOREST {_stage}-{_subStage}";
    }

    public void SetFarmMode()
    {
        FarmMode = true;
        _stageSlider.gameObject.SetActive(false);
    }

    public void OnSubstageCompleted()
    {
        if (!FarmMode)
        {
            _subStage++;
            if (_subStage >= MaxAmountOfSubStagesPerStage)
            {
                _subStage = 1;
                _stage++;
            }
            _stageSlider.value = SliderStageValues[0];
            UpdateText();
            _fightNumber = 1;
        }
    }

    public int GetStage(){
        return _stage;
    }

    public int GetSubStage(){
        return _subStage;
    }
}
