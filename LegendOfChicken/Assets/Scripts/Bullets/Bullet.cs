using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public abstract class Bullet : MonoBehaviour
{
    public event Action<IGetDamagable> GetDamaged;

    [SerializeField] protected float _speed;

    protected IDamage _damage;

    [SerializeField] private ParticleSystem _destroyEffect;

    protected Vector3 _targetPosition;
    protected IGetDamagable _getDamagableObject;

    protected Tween _moveTween;

    private Coroutine _coroutine;

    private bool _isCritHit;

    [field: SerializeField] public bool CanInteract { get; private set; }

    public virtual void Initialize(IDamage damage, Vector3 initPos, Vector3 targetPos, IGetDamagable getDamagableObject)
    {
        gameObject.SetActive(true);
        CanInteract = false;
        _isCritHit = false;

        transform.position = initPos;
        _targetPosition = targetPos;
        _getDamagableObject = getDamagableObject;

        _damage = damage;

        GetDamaged += (IGetDamagable getDamagable) => {
            if (!CanInteract)
            {
                getDamagable.GetDamage(_damage);
                CanInteract = true;
            }
        };

        MoveBullet();
    }

    protected virtual void MoveBullet()
    {
        if(_coroutine == null)
            _coroutine = StartCoroutine(WaitBulletUntilTargetPosition());
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(WaitBulletUntilTargetPosition());
        }
    }

    protected virtual IEnumerator WaitBulletUntilTargetPosition()
    {
        WaitUntil waitUntil = new WaitUntil(() => transform.position == _targetPosition);

        yield return waitUntil;

        _moveTween.Kill();

        if(_getDamagableObject != null)
            InvokeGetDamage(_getDamagableObject);

        if(_destroyEffect != null)
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);

        CanInteract = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void InvokeGetDamage(IGetDamagable target)
    {
        if (target.IsDied)
            return;

        GetDamaged?.Invoke(_getDamagableObject);
    }

    private void OnDisable()
    {
        _moveTween.Kill();
    }

    private void OnDestroy()
    {
        _moveTween.Kill();
    }
}
