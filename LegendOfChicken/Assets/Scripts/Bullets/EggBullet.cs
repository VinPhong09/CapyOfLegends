using UnityEngine;
using DG.Tweening;

public class EggBullet : Bullet
{
    public override void Initialize(IDamage damage, Vector3 initPos, Vector3 targetPos, IGetDamagable getDamagable)
    {
        base.Initialize(damage, initPos, targetPos, getDamagable);
    }

    protected override void MoveBullet()
    {
        base.MoveBullet();
        _moveTween = transform.DOJump(_targetPosition, 0.4f, 1, _speed).SetEase(Ease.Linear);
    }
}
