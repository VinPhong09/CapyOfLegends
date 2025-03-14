using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public readonly int WalkLabel = Animator.StringToHash("Walk");
    public readonly int AttackLabel = Animator.StringToHash("Attack");

    public void PlayAnimationByLabel(int stringHash, bool value)
    {
        _animator.SetBool(stringHash, value);
    }
}
