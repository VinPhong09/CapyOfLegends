using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Chicken : MonoBehaviour, IPointerClickHandler, IGetDamagable
{
    public event Action Clicked;
    public event Action Died;

    private float _health;

    [SerializeField] private UnityEvent<float, float> GotDamage;

    [SerializeField] private ScrollEnhancesElement _scrollEnhancesElement;
    [SerializeField] private ChickenParameters _chickenParams;
    [SerializeField] private EnhanceElementParameters _enhanceElementParameters;
    [SerializeField] private ParticleSystem _enhanceEffect;
    [SerializeField] private ParticleSystem _featherEffect;
    [SerializeField] private DamageInfo _damageInfo;

    public ScrollEnhancesElement ScrollEnhancesElement => _scrollEnhancesElement;
    public ChickenParameters ChickenParams => _chickenParams;

    public bool IsDied { get; set; }

    public void Initialize(Vector3 pos)
    {
        _health = _chickenParams.Health;

        _enhanceElementParameters.Value = _chickenParams.Health;

        transform.position = pos;

        Died += () => {
            Instantiate(_featherEffect, transform.position, Quaternion.identity);
            ChickensController.Instance.RemoveChicken(this);
        };

        IsDied = false;
    }

    public void GetDamage(IDamage damage)
    {
        DamageInfo damageInfo = Instantiate(_damageInfo);
        damageInfo.Initialize((int)damage.TotalDamage, damage.CriticalDamage ? Color.red : Color.white, transform.position);

        if (_health - damage.TotalDamage <= 0)
        {
            IsDied = true;
            Died?.Invoke();
            Destroy(gameObject);
            return;
        }

        _health -= damage.TotalDamage;

        GotDamage?.Invoke(_chickenParams.Health, _health);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<ChickenAnimator>().OnPlaySelectedAnimation();

        if(ChickensController.Instance)
            ChickensController.Instance.SelectChicken(this);

        Clicked?.Invoke();
    }

    public void PlayEnhanceEffect()
    {
        if (_enhanceEffect.isPlaying)
            _enhanceEffect.Stop();
        _enhanceEffect.Play();
    }

    public void OnEnhanceHealth()
    {
        _chickenParams.Health += 10;

        _enhanceElementParameters.Value = _chickenParams.Health;
    }
}
