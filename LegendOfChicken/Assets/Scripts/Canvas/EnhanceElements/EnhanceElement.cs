using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnhanceElement : MonoBehaviour
{
    private event Action Enhanced;

    [SerializeField] private EnhanceElementParameters _enhanceElementParameters;
    [SerializeField] private float _delayBeforeEnhance;
    [SerializeField] private float _scaleButtonDuraction;
    [Range(0, 1f)]
    [SerializeField] private float _buttonEnhanceScale;

    private ChickenParameters _chickenParameters;

    [Header("Parameters")]
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _labelTxt;
    [SerializeField] private TextMeshProUGUI _levelTxt;
    [SerializeField] private TextMeshProUGUI _valueTxt;
    [SerializeField] private TextMeshProUGUI _costTxt;
    [SerializeField] private GameObject _enhanceButton;

    private Coroutine _enhancingCoroutine;

    public void Initialize(ChickenParameters chickenParameters)
    {
        _chickenParameters = chickenParameters;

        Enhanced += _enhanceElementParameters.OnEnhance;
        Enhanced += () =>
        {
            ChickensController.Instance.PlaySelectedEnhancedChickenEffect();
            _enhanceElementParameters.Level++;
            UpdateViewData();
        };

        UpdateViewData();
    }

    public void OnStartEnhance()
    {
        if(_enhancingCoroutine == null)
            _enhancingCoroutine = StartCoroutine(nameof(Enhancing));
    }

    public void OnStopEnhance()
    {
        if(_enhancingCoroutine != null)
        {
            StopCoroutine(_enhancingCoroutine);
            _enhancingCoroutine = null;
        }

        OnEnhance();

        _enhanceButton.transform.DOScale(Vector3.one, _scaleButtonDuraction);
    }

    private IEnumerator Enhancing()
    {
        _enhanceButton.transform.DOScale(new Vector3(_buttonEnhanceScale, _buttonEnhanceScale, 1f), _scaleButtonDuraction);

        yield return new WaitForSeconds(_delayBeforeEnhance);

        var waitForSecs = new WaitForSeconds(_scaleButtonDuraction);

        while (true)
        {
            _enhanceButton.transform.DOScale(Vector3.one, _scaleButtonDuraction);
            OnEnhance();

            yield return waitForSecs;

            _enhanceButton.transform.DOScale(new Vector3(_buttonEnhanceScale, _buttonEnhanceScale, 1f), _scaleButtonDuraction);

            yield return waitForSecs;
        }
    }

    private void OnEnhance()
    {
        //if(CoinsView.Instance)
        CurrencyView.Instance.TrySpendCoins(Mathf.CeilToInt(_enhanceElementParameters.Cost), () => Enhanced?.Invoke(), () => { print("enhanced failed"); });

        //Enhanced?.Invoke();
    }

    private void UpdateViewData()
    {
        _iconImg.sprite = _enhanceElementParameters.Icon;
        _labelTxt.text = _enhanceElementParameters.Label;
        _levelTxt.text = $"Lvl {_enhanceElementParameters.Level}";
        _valueTxt.text = _enhanceElementParameters.Value.ToString();
        _costTxt.text = Mathf.CeilToInt(_enhanceElementParameters.Cost).ToString();
    }
}
