using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Currency", menuName = "ScriptableObjects/Currencies/Currency")]
public class CurrencySO : ScriptableObject
{
    public string _name;
    public int _amount;
    public Sprite _currencyIconImage;
}

