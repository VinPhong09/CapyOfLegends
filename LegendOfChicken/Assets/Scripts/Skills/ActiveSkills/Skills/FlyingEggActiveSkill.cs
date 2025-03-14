using UnityEngine;
using DG.Tweening;

public class FlyingEggActiveSkill : ActiveSkill
{
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private SpriteRenderer _flyingEgg;

    [SerializeField] private Vector3 _containerPos;
    [SerializeField] private float _flyToPointReadySpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _flyingEggFallSpeed;

    [Header("Meteors Poses")]
    [SerializeField] private Vector3 _initPoint;
    [SerializeField] private Vector3 _readyPoint;
    [SerializeField] private Vector3 _endPoint;

    private Tween _tween;

    public override void Initialize()
    {
        transform.position = _containerPos;

        SpriteRenderer flyingEgg = Instantiate(_flyingEgg, _initPoint, Quaternion.Euler(0f, 0f, -35f));
        flyingEgg.transform.SetParent(transform);

        IDamage damage = new IDamage();
        damage.NumberDamage = 35f;

        _tween = DOTween.Sequence()
            .Append(flyingEgg.transform.DOLocalMove(_readyPoint, _flyToPointReadySpeed))
            .Append(flyingEgg.transform.DOLocalRotate(Vector3.zero, _rotationSpeed)).SetEase(Ease.Linear)
            .Append(flyingEgg.transform.DOLocalMove(_endPoint, _flyingEggFallSpeed)).SetEase(Ease.Linear)
            .AppendCallback(() => EnemiesSpawner.Instance.SplashDamageToSpawnedEnemies(damage))
            .AppendCallback(() => Instantiate(_explosion, flyingEgg.transform.position, _explosion.transform.rotation))
            .AppendCallback(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        if (_tween != null)
            _tween.Kill();
    }
}

