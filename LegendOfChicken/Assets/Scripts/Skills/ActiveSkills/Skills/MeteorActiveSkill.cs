using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MeteorActiveSkill : ActiveSkill
{
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private SpriteRenderer _meteor;

    [SerializeField] private Vector3 _containerPos;
    [SerializeField] private int _amount;
    [SerializeField] private float _delay;
    [SerializeField] private float _meteorsFallSpeed;

    [Header("Meteors Poses")]
    [SerializeField] private float _pointX1;
    [SerializeField] private float _pointX2;
    [SerializeField] private float _pointY;
    [SerializeField] private float _pointY2;
    [SerializeField] private float _offset;

    private Coroutine _createMeteors;

    private List<SpriteRenderer> _meteors = new List<SpriteRenderer>();

    private Tween _tween;

    public override void Initialize()
    {
        transform.position = _containerPos;

        for (int i = 0; i < _amount; i++)
        {
            SpriteRenderer meteor = Instantiate(_meteor);
            _meteors.Add(meteor);

            meteor.transform.SetParent(transform);
            meteor.transform.position = new Vector3(Random.Range(_pointX1, _pointX2), _pointY, 0f);
            meteor.gameObject.SetActive(false);
        }

        if (_createMeteors == null)
            _createMeteors = StartCoroutine(CreateMeteors());
        else
        {
            StopCoroutine(_createMeteors);
            _createMeteors = StartCoroutine(CreateMeteors());
        }
    }

    private IEnumerator CreateMeteors()
    {
        var waitForSecs = new WaitForSeconds(_delay);

        for (int i = 0; i < _meteors.Count; i++)
        {
            _meteors[i].gameObject.SetActive(true);

            IDamage damage = new IDamage();
            damage.NumberDamage = 25f;

            DOTween.Sequence()
                .Append(_meteors[i].transform.DOMove(new Vector3(_meteors[i].transform.position.x + _offset, _pointY2, 0f), _meteorsFallSpeed)).SetEase(Ease.Linear)
                .AppendCallback(() => Instantiate(_explosion, _meteors[i].transform.position, Quaternion.Euler(-90f, 0f, 0f)))
                .AppendCallback(() => EnemiesSpawner.Instance.SplashDamageToSpawnedEnemies(damage))
                .AppendCallback(() => _meteors[i].gameObject.SetActive(false));

            yield return waitForSecs;
        }

        _createMeteors = null;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_tween != null)
            _tween.Kill();
    }
}
