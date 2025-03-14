using System;
using UnityEngine;

public abstract class Boss : Enemy
{
    [SerializeField] private BossParameters _bossParameters;
    [SerializeField] private EnemyAnimator _bossAnimator;
    [SerializeField] private DamageInfo _damageInform;

    public BossParameters Parameters => _bossParameters;

    public override void Initialize(Vector3 initPos, Action diedActions, float difficultyRatio)
    {
        transform.position = initPos;
        _health = _bossParameters.Health * difficultyRatio;
        Died += diedActions;
        Died += () =>
        {
            CurrencyView.Instance.CreateCoinCollectedImage(Camera.main.WorldToScreenPoint(transform.position), UnityEngine.Random.Range(5, 15));
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        };

        _bossAnimator.PlayAnimationByLabel(_bossAnimator.WalkLabel, true);

        IsDied = false;

        StartEnter();
    }

    public override void GetDamage(IDamage damage)
    {
        DamageInfo damageInfo = Instantiate(_damageInform);
        damageInfo.Initialize((int)damage.TotalDamage, damage.CriticalDamage ? Color.red : Color.white, transform.position);

        if ((_health - damage.TotalDamage <= 0) && !IsDied)
        {
            IsDied = true;
            InvokeDied();
            Destroy(gameObject);
            return;
        }

        _health -= damage.TotalDamage;

        if (!_hpBar.activeInHierarchy)
            _hpBar.SetActive(true);

        GotDamage?.Invoke(_bossParameters.Health, _health);
    }
}
