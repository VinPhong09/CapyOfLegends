using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Image[] _allButtonPanels;
    [SerializeField] private Sprite _selectedImg;
    [SerializeField] private Sprite _nonSelected;

    [SerializeField] private List<Button> _buttonSummon15;
    [SerializeField] private List<Button> _buttonSummon35;

    private void Awake()
    {
        SummonElementsPanel.OnSummon += CheckInteractableButtons;
    }

    public void OnSelectPanel(Image selected)
    {
        foreach (Image i in _allButtonPanels)
        {
            i.sprite = _nonSelected;
        }
        selected.sprite = _selectedImg;
    }
    
    public void OnEnable()
    {
        CheckInteractableButtons();
    }
    

    public void CheckInteractableButtons()
    {
        var gems = CurrencyView.Instance.Gems;
        Debug.Log("Checking interactable buttons - " + gems);
        
        var canSummon15 = gems >= 500;
        var canSummon35 = gems >= 1500;
        
        HandleButtonSummon(canSummon15, _buttonSummon15);
        HandleButtonSummon(canSummon35, _buttonSummon35);
    }

    public void HandleButtonSummon(bool setActive, List<Button> buttons)
    {
            foreach (var btn in buttons)
            {
                btn.interactable = setActive;
            }
    }
    /*public void OpenPanel(GameObject panel)
    {

    }*/
}
