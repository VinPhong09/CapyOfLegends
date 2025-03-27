using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SummonElementsPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SummonElement[] _cards;
    
    [Header("Equipment Elements")]
    [SerializeField] private WeaponEquipmentElementParameters[] _weaponEquipmentElements;
    [SerializeField] private HatEquipmentElementParameters[] _hatEquipmentElements;
    [SerializeField] private ArmorEquipmentElementParameters[] _armorEquipmentElements;

    [Header("Buttons")]
    [SerializeField] private GameObject _weaponButtons;
    [SerializeField] private GameObject _hatsButtons;
    [SerializeField] private GameObject _shieldsButtons;

    private readonly float DelayTime = 0.035f;

    private Coroutine _appearElements;

    private List<ChickenEquipmentElementParameters> _generatedSummons = new List<ChickenEquipmentElementParameters>();

    private bool _canSummons;
    
    public static Action OnSummon;

    private void OnDisable()
    {
        DisableAllCards();
    }

    public void CreateWeaponsAd(int amount)
    {
        AdmodManager.Instance.ShowRewardedAd(CreateWeapons, amount);
    }
    
    public void CreateHatsAd(int amount)
    {
        AdmodManager.Instance.ShowRewardedAd(CreateHats, amount);
    }
    
    public void CreateShieldsAd(int amount)
    {
        AdmodManager.Instance.ShowRewardedAd(CreateShields, amount);
    }
    
    
    public void CreateWeapons(int amount)
    { 
       if(_canSummons) 
           Initialize(amount, IEquipmentType.EquipmentType.Weapon);
       else
       {
           Debug.Log("Can't create weapons while can summons");
       }
    }

    public void CreateHats(int amount)
    {
        if(_canSummons) 
            Initialize(amount, IEquipmentType.EquipmentType.Hat);
    }

    public void CreateShields(int amount)
    {
        if(_canSummons) 
            Initialize(amount, IEquipmentType.EquipmentType.Armor);
    }

    public void Initialize(int amount, IEquipmentType.EquipmentType equipmenType)
    {
        if (amount >= 35)
            amount = 35;

        if (_generatedSummons.Count > 0)
            _generatedSummons.Clear();

        DisableAllCards();

        switch (equipmenType)
        {
            case IEquipmentType.EquipmentType.Armor:
                for (int i = 0; i < amount; i++)
                {
                    _weaponButtons.SetActive(false);
                    _hatsButtons.SetActive(false);
                    _shieldsButtons.SetActive(true);

                    ChickenEquipmentElementParameters generatedElement = _armorEquipmentElements[Random.Range(0, _armorEquipmentElements.Length)];
                    generatedElement.Progress++;
                    _generatedSummons.Add(generatedElement);
                }
                break;
            case IEquipmentType.EquipmentType.Weapon:
                for (int i = 0; i < amount; i++)
                {
                    _weaponButtons.SetActive(true);
                    _hatsButtons.SetActive(false);
                    _shieldsButtons.SetActive(false);

                    ChickenEquipmentElementParameters generatedElement = _weaponEquipmentElements[Random.Range(0, _weaponEquipmentElements.Length)];
                    generatedElement.Progress++;
                    _generatedSummons.Add(generatedElement);
                }
                break;
            case IEquipmentType.EquipmentType.Hat:
                for (int i = 0; i < amount; i++)
                {
                    _weaponButtons.SetActive(false);
                    _hatsButtons.SetActive(true);
                    _shieldsButtons.SetActive(false);

                    ChickenEquipmentElementParameters generatedElement = _hatEquipmentElements[Random.Range(0, _hatEquipmentElements.Length)];
                    generatedElement.Progress++;
                    _generatedSummons.Add(generatedElement);
                }
                break;
        }

        if (_appearElements == null)
            _appearElements = StartCoroutine(AppearElements(amount));
        else
        {
            StopCoroutine(_appearElements);
            _appearElements = StartCoroutine(AppearElements(amount));
        }
        OnSummon?.Invoke();
    }

    private IEnumerator AppearElements(int amount)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(DelayTime);

        for(int i = 0; i < amount; i++)
        {
            _cards[i].gameObject.SetActive(true);
            _cards[i].Initialize(_generatedSummons[i]);
            yield return waitForSeconds;
        }
    }

    private void DisableAllCards()
    {
        foreach (SummonElement i in _cards)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void CheckCurrencyGems(int amount)
    {
        var gems = CurrencyView.Instance.Gems;
        
        if (gems >= amount)
        {
            CurrencyView.Instance.SpendGems(amount);
            _canSummons = true;
            return;
        }
        _canSummons = false;
        this.gameObject.SetActive(false);
    }
}
