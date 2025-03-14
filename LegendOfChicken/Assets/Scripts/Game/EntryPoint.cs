using UnityEngine;
using UnityEngine.Events;
using GameAnalyticsSDK;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent[] GameEnteredActions;

    private void Awake()
    {
		GameAnalytics.Initialize();
		
        for (int i = 0; i < GameEnteredActions.Length; i++)
        {
            GameEnteredActions[i]?.Invoke();
        }
    }
}
