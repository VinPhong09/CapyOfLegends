using UnityEngine;

public abstract class AttackAction : MonoBehaviour, IAttackable
{
    [SerializeField] protected Bullet _defaultBullet;

    public abstract void Attack();
}
