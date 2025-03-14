using UnityEngine;
using UnityEngine.Events;

public class TransitionBetweenLevels : MonoBehaviour
{
    [SerializeField] private AnimatorBasicActions _animatorBasicActions;

    public void Initialize(UnityAction action)
    {
        _animatorBasicActions.AddAnimationCompleteAction(action);
        gameObject.SetActive(true);
    }
}
