using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public abstract class Enemy : MonoBehaviour, IGetDamagable
{
    [SerializeField] protected UnityEvent<float, float> GotDamage;
    
    public event Action Died;

    [SerializeField] private EnemyParameters _enemyParameters;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] protected ParticleSystem _deathEffect;
    [SerializeField] protected GameObject _hpBar;
    [SerializeField] private DamageInfo _damageInfo;

    protected readonly float MinEnterDiraction = 1f;
    protected readonly float MaxEnterDiraction = 2f;
    
    protected float _health;

    private float _enterTime = 6.0f;

    private readonly float TargetMeleePos = -0.4f;

    public EnemyParameters EnemyParameters => _enemyParameters;

    public bool IsDied { get; set; }

    private Tween _tweenMove;

    public virtual void Initialize(Vector3 initPos, Action diedActions, float difficultyRatio)
    {
        transform.position = initPos;
        _health = _enemyParameters.Health * difficultyRatio;
        Died += diedActions;
        Died += () =>
        {
            CurrencyView.Instance.CreateCoinCollectedImage(Camera.main.WorldToScreenPoint(transform.position), UnityEngine.Random.Range(2, 6));
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        };

        _animator.PlayAnimationByLabel(_animator.WalkLabel, true);

        IsDied = false;

        StartEnter();
    }

    protected void StartEnter()
    {
        //Vector2 endPosition = new Vector2(transform.position.x - UnityEngine.Random.Range(MinEnterDiraction, MaxEnterDiraction), transform.position.y);
        Vector2 endPosition = new Vector2(TargetMeleePos, transform.position.y);

        _tweenMove = DOTween.Sequence()
            .Append(transform.DOMoveX(endPosition.x, _enterTime).SetEase(Ease.Linear))
            .AppendCallback(() => _animator.PlayAnimationByLabel(_animator.AttackLabel, true));
    }

    public virtual void GetDamage(IDamage damage)
    {
        DamageInfo damageInfo = Instantiate(_damageInfo);
        damageInfo.Initialize((int)damage.TotalDamage, damage.CriticalDamage ? Color.red : Color.white, transform.position);

        if ((_health - damage.TotalDamage <= 0) && !IsDied)
        {
            IsDied = true;
            Died?.Invoke();
            AlongWayTaskController.Instance.OnEnemyDied();
            Destroy(gameObject);
            return;
        }

        _health -= damage.TotalDamage;

        if(!_hpBar.activeInHierarchy)
            _hpBar.SetActive(true);

        GotDamage?.Invoke(_enemyParameters.Health, _health);
    }

    protected void InvokeDied()
    {
        Died?.Invoke();
    }

    private void OnDestroy()
    {
        _tweenMove.Kill();
    }
}