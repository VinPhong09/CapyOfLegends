using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlongWayTaskController : MonoBehaviour
{
    public static AlongWayTaskController Instance { get; private set; }

    [SerializeField] private AlongWayTaskList _taskListSO;
    private List<AlongWayTask> _taskList;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private Image _rewardCurrencyIcon;
    [SerializeField] private GameObject _notifyPoint;
    [SerializeField] private Animator _taskAnimator; 

    private int _taskProgress = 0;
    private bool _taskComplete = false;
    private int _taskNumber = 0;
    private int _taskCycleCounter = 0;
    private int _taskGoal;
    private int _taskReward;
    private AlongWayTask currentTask;
    private int _targetStage;
    private int _targetSubStage;
    
    
    private void NextTaskUpdate(){
        
        _taskProgress = 0;

        if (_taskList.Count < _taskNumber + 1){
            _taskNumber = 0;
            _taskCycleCounter++;
        }
        currentTask = _taskList[_taskNumber];

        switch ((int)currentTask.taskType){
            case 0:
                _taskGoal = currentTask._goal * (_taskCycleCounter + 1);
                _taskReward = currentTask._rewardAmount + 10 * _taskCycleCounter;
                _descriptionText.text = currentTask._text + " (" + _taskProgress.ToString() + "/" +  _taskGoal.ToString() + ")";
                _rewardAmountText.text = _taskReward.ToString();
                break;
            case 1:
                _targetStage = _taskCycleCounter + 1;
                _targetSubStage = Random.Range(1, (StageController.Instance.MaxAmountOfSubStagesPerStage - 6));
                
                _taskGoal = currentTask._goal;
                _taskReward = currentTask._rewardAmount + 10 * _taskCycleCounter;
                _descriptionText.text = currentTask._text + "\n" + _targetStage.ToString() + " - " + _targetSubStage.ToString() + " (" + _taskProgress.ToString() + "/" +  _taskGoal.ToString() + ")";
                _rewardAmountText.text = _taskReward.ToString();
                TargetStageCompleteCheck();
                break;
        }

        _taskAnimator.SetBool("Shifted", false);
    }

    private void Awake()
    {
        Instance = this;

        _taskList = _taskListSO._taskList;
        NextTaskUpdate();
    }
  
    public void OnEnemyDied()
    {
        if((int)_taskList[_taskNumber].taskType == 0){
            _taskProgress++;
            ProgressUpdate();
            ProgressCheck();
        }
    }

    public void ProgressUpdate(){
        _descriptionText.text = currentTask._text + " (" + _taskProgress.ToString() + "/" + _taskGoal.ToString() + ")";
    }

    private void ProgressCheck(){
        if (_taskProgress >= _taskGoal){
            _taskComplete = true;
            _notifyPoint.SetActive(true);
        }
    }

    public void TargetStageCompleteCheck(){
        if ((_targetStage == StageController.Instance.GetStage() && _targetSubStage < StageController.Instance.GetSubStage()) || _targetStage < StageController.Instance.GetStage()){
            _taskProgress++;
            ProgressUpdate();
            ProgressCheck();
        }
    }

    public void OnClick(){
        if (_taskComplete){
            _taskAnimator.SetBool("Shifted", true);
            _taskComplete = false;
            CurrencyView.Instance.CreditCurrency(currentTask._rewardCurrency, _taskReward);
            _notifyPoint.SetActive(false);
            _taskNumber++;
            
            Invoke("NextTaskUpdate", 0.4f);
        }
    }
    
}
