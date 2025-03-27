using System;
using UnityEngine;
using TMPro;

public class CurrencyView : MonoBehaviour
{
    public static CurrencyView Instance;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private RectTransform _iconPosition;
    [SerializeField] private CoinCollectedImage _coinCollectedImage;
    [SerializeField] private Canvas _canvasUpdating;
    [SerializeField] private CurrencySO _coins;

    [Header("Gems")]
    [SerializeField] private TextMeshProUGUI _gemText;
    [SerializeField] private Transform _gemsIcon;
    [SerializeField] private CurrencySO _gems;
    

    //private int _coins;
    //private int _gems = 0;

    public int Coins => _coins._amount;
    public int Gems => _gems._amount;

    private void OnEnable()
    {
        Instance = this;

        UpdateUI();

    }

    public void CreditCoins(int amount)
    {
        if (amount < 0)
            return;

        _coins._amount += amount;
        UpdateUI();
    }
    // Пока так. Думаю надо сделать общий метод для всех валют и передавать в него идентификатор
    public void CreditCurrency(CurrencySO currency, int amount)
    {
        if (amount < 0)
            return;

        currency._amount += amount;
        UpdateUI();
    }
    
    public void CreditGems(int amount)
    {
        if (amount < 0)
            return;

        _gems._amount += amount;
        UpdateUI();
    }

    public void TrySpendCoins(int cost, Action SuccessSpendCoins, Action FailedSpendCoins)
    {
        if(_coins._amount >= cost)
        {
            _coins._amount -= cost;
            UpdateUI();
            SuccessSpendCoins?.Invoke();
            return;
        }

        FailedSpendCoins?.Invoke();
    }

    public void SpendGems(int amount)
    {
        if (_gems._amount < amount) return;
        
        _gems._amount -= amount;
        UpdateUI();
    }
    public void CreateCoinCollectedImage(Vector3 initPos, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            CoinCollectedImage coinCollectedImage = Instantiate(_coinCollectedImage);
            coinCollectedImage.transform.SetParent(_canvasUpdating.transform);
            coinCollectedImage.Initialize(initPos, _iconPosition.position, 10);
        }
    }

    private void GemCollectedAnimation(){

    }

    private void UpdateUI()
    {
        _coinText.text = _coins._amount.ToString();
        _gemText.text = _gems._amount.ToString();
    }

    public void CurrencyRestart(){
        _coins._amount = 0;
        _gems._amount = 0;
    }

}
