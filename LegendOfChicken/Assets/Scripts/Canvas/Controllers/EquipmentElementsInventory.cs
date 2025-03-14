using System.Collections.Generic;
using UnityEngine;

public class EquipmentElementsInventory : MonoBehaviour
{
    [SerializeField] private EquipmentElement[] _equipmentElementsWeapon;
    [SerializeField] private EquipmentElement[] _equipmentElementsHat;
    [SerializeField] private EquipmentElement[] _equipmentElementsArmor;

    [SerializeField] private EnhancedElementsPanel _enhancedElementsPanel;

    public void OnEnable()
    {
        UpdateCardsInfo();
    }

    public void UpdateCardsInfo()
    {
        foreach (EquipmentElement i in _equipmentElementsWeapon)
        {
            i.UpdateInfo();
        }

        foreach (EquipmentElement i in _equipmentElementsHat)
        {
            i.UpdateInfo();
        }

        foreach (EquipmentElement i in _equipmentElementsArmor)
        {
            i.UpdateInfo();
        }
    }

    public void EnhanceAllWeapon()
    {
        List<ChickenEquipmentElementParameters> _enhancesElements = new List<ChickenEquipmentElementParameters>();

        foreach (EquipmentElement i in _equipmentElementsWeapon)
        {
            if (i.EquipmentElementParameters.GetElementIfCanEnhance() != null)
                _enhancesElements.Add(i.EquipmentElementParameters);

            i.EquipmentElementParameters.CheckLevelToEnhance();
            i.CheckForEnhance();
            i.UpdateInfo();
        }

        if (_enhancesElements.Count <= 0)
            return;

        _enhancedElementsPanel.Initialize(_enhancesElements.ToArray());
        _enhancedElementsPanel.gameObject.SetActive(true);
    }

    public void EnhanceAllHat()
    {
        List<ChickenEquipmentElementParameters> _enhancesElements = new List<ChickenEquipmentElementParameters>();

        foreach (EquipmentElement i in _equipmentElementsHat)
        {
            if (i.EquipmentElementParameters.GetElementIfCanEnhance() != null)
                _enhancesElements.Add(i.EquipmentElementParameters);

            i.EquipmentElementParameters.CheckLevelToEnhance();
            i.CheckForEnhance();
            i.UpdateInfo();
        }

        if (_enhancesElements.Count <= 0)
            return;

        _enhancedElementsPanel.Initialize(_enhancesElements.ToArray());
        _enhancedElementsPanel.gameObject.SetActive(true);
    }

    public void EnhanceAllArmor()
    {
        List<ChickenEquipmentElementParameters> _enhancesElements = new List<ChickenEquipmentElementParameters>();

        foreach (EquipmentElement i in _equipmentElementsArmor) 
        {
            if (i.EquipmentElementParameters.GetElementIfCanEnhance() != null)
                _enhancesElements.Add(i.EquipmentElementParameters);

            i.EquipmentElementParameters.CheckLevelToEnhance();
            i.CheckForEnhance();
            i.UpdateInfo();
        }

        if (_enhancesElements.Count <= 0)
            return;

        _enhancedElementsPanel.Initialize(_enhancesElements.ToArray());
        _enhancedElementsPanel.gameObject.SetActive(true);
    }
}
