using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChickenAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _selectPointer;

    private readonly int RunLabel = Animator.StringToHash("Run");
    private readonly int AttackLabel = Animator.StringToHash("Attack");

    public void OnPlayAttackAnimation()
    {
        _animator.SetTrigger(AttackLabel);
    }

    public void OnPlaySelectedAnimation()
    {
        _selectPointer.SetActive(true);
    }

    public void OnStopSelectedAnimation()
    {
        _selectPointer.SetActive(false);
    }

    public void OnPlayRunAnimation()
    {
        _animator.SetBool(RunLabel, true);
    }

    public void OnPlayIdleAnimation()
    {
        _animator.SetBool(RunLabel, false);
    }
}
