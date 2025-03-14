using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StageTransitionController : MonoBehaviour
{
    [SerializeField] private UnityEvent NextStageMoveStarted;
    [SerializeField] private UnityEvent NextStageMoveCompleted;

    [SerializeField] private float _transitionTimeMin;
    [SerializeField] private float _transitionTimeMax;

    public void OnNextStageMoveStart()
    {
        if ((StageController.Instance.FightNumber >= StageController.Instance.MaxAmountOfFightsPerSubStage) && !StageController.Instance.FarmMode)
        {
            StageController.Instance.StageComplete();
            return;
        }

        NextStageMoveStarted?.Invoke();

        Invoke(nameof(OnNextStageMoveComplete), Random.Range(_transitionTimeMin, _transitionTimeMax));
    }

    public void OnNextStageMoveComplete()
    {
        NextStageMoveCompleted?.Invoke();
    }
}
