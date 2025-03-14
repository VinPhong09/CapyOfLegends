using UnityEngine;
using DG.Tweening;

public class StraightFlyingBullet : Bullet
{
    public override void Initialize(IDamage damage, Vector3 initPos, Vector3 targetPos, IGetDamagable getDamagable)
    {
        base.Initialize(damage, initPos, targetPos, getDamagable);
    }

    protected override void MoveBullet()
    {
        base.MoveBullet();
        _moveTween = transform.DOMove(_targetPosition, _speed).SetEase(Ease.Linear);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y, 
            Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
    }
}
