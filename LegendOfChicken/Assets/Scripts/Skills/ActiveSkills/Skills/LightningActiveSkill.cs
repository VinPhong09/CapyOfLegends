using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LightningActiveSkill : ActiveSkill
{
    [SerializeField] private ParticleSystem _lightning;

    [SerializeField] private Vector3 _containerPos;
    [SerializeField] private int _amount;
    [SerializeField] private float _delay;

    private Coroutine _createLightnings;

    private Tween _tween;

    public override void Initialize()
    {
        transform.position = _containerPos;

        if (_createLightnings == null)
            _createLightnings = StartCoroutine(CreateLightnings());
        else
        {
            StopCoroutine(_createLightnings);
            _createLightnings = StartCoroutine(CreateLightnings());
        }
    }

    private IEnumerator CreateLightnings()
    {
        var waitForSecs = new WaitForSeconds(_delay);

        for (int i = 0; i < _amount; i++)
        {
            if(EnemiesSpawner.Instance.TryGetRandomEnemy(out Enemy enemy))
            {
                ParticleSystem lightning = Instantiate(_lightning, enemy.transform.position, _lightning.transform.rotation);
                lightning.transform.SetParent(transform);

                IDamage damage = new IDamage();
                damage.NumberDamage = 20f;

                enemy.GetDamage(damage);
            }

            yield return waitForSecs;
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_tween != null)
            _tween.Kill();
    }
}
