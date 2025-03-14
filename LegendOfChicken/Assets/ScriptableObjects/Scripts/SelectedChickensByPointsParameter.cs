using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Selected Chickens", menuName = "ScriptableObjects/Gameplay Elements/Selected Chickens")]
public class SelectedChickensByPointsParameter : ScriptableObject
{
    [SerializeField] private Chicken[] _selectedChickens;

    public void SetChickenByIndex(int index, Chicken chicken)
    {
        if (index >= _selectedChickens.Length)
            return;

        _selectedChickens[index] = chicken;
    }

    public Chicken GetChickenByIndex(int index)
    {
        if (index >= _selectedChickens.Length)
            return null;

        return _selectedChickens[index];
    }

    public int GetIndexByChicken(Chicken chicken)
    {
        for(int i = 0; i < _selectedChickens.Length; i++)
        {
            if (_selectedChickens[i] == null)
                continue;

            if (_selectedChickens[i].ChickenParams.Index == chicken.ChickenParams.Index)
                return i;
        }

        return 0;
    }

    public bool TryFindSelectedChickenByIndex(int index)
    {
        foreach (Chicken i in _selectedChickens)
        {
            if (i == null)
                continue;

            if (i.ChickenParams.Index == index)
                return false;
        }

        return true;
    }

    public int GetAmountOfChickens()
    {
        int amount = 0;

        foreach(Chicken i in _selectedChickens)
        {
            if (i == null)
                continue;

            amount++;
        }

        return amount;
    }

    public ChickenParameters[] GetActiveChickens()
    {
        List<ChickenParameters> chickenParameters = new List<ChickenParameters>();

        foreach (Chicken i in _selectedChickens)
        {
            if (i == null)
                continue;
            chickenParameters.Add(i.ChickenParams);
        }
        return chickenParameters.ToArray();
    }

    public void RemoveChickenByIndex(int index)
    {
        if (index >= _selectedChickens.Length)
            return;

        _selectedChickens[index] = null;
    }
}
