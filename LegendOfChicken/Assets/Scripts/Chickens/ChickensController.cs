using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ChickensController : MonoBehaviour
{
    public static ChickensController Instance;

    [SerializeField] private UnityEvent<ScrollEnhancesElement, ChickenParameters, Action> ChickenClicked;
    [SerializeField] private UnityEvent ChickenReplaced;
    [SerializeField] private UnityEvent<int> ChickenDied;

    [SerializeField] private Transform[] _chickenPositions;
    [SerializeField] private SelectedChickensByPointsParameter _selectedChickensByPointsParameter;

    private List<Chicken> _createdChickens = new List<Chicken>();

    private Chicken _selectedChicken;

    private readonly float DuractionToMoveScreenOut = 1f;
    private readonly float PositionToMoveScreenOut = 4f;

    public int ChickensAmount => _createdChickens.Count;

    private void OnEnable()
    {
        Instance = this;
    }

    public void CreateChicken()
    {
        ClearAllChickens();

        ChickenReplaced?.Invoke();

        for (int i = 0; i < _chickenPositions.Length; i++)
        {
            if (_selectedChickensByPointsParameter.GetChickenByIndex(i) == null)
                continue;
            Chicken chicken = Instantiate(_selectedChickensByPointsParameter.GetChickenByIndex(i));
            chicken.Initialize(_chickenPositions[i].transform.position);
            _createdChickens.Add(chicken);

            chicken.Clicked += () =>
            {
                ChickenClicked?.Invoke(chicken.ScrollEnhancesElement, chicken.ChickenParams, () => 
                {
                    if (chicken == null)
                        return;
                    chicken.GetComponent<ChickenAnimator>().OnStopSelectedAnimation();    
                });
            };
        }

        ActiveSkillsController.Instance.UpdateActiveSkillsList(_selectedChickensByPointsParameter.GetActiveChickens());
        ActiveSkillsController.Instance.UpdateActiveSkills();
        RenderViewController.Instance.Invoke();
    }

    private void ClearAllChickens()
    {
        foreach(Chicken i in _createdChickens)
        {
            Destroy(i.gameObject);
        }

        _createdChickens.Clear();
    }

    public void SelectChicken(Chicken chicken)
    {
        _selectedChicken = chicken;
    }

    public void PlaySelectedEnhancedChickenEffect()
    {
        _selectedChicken.PlayEnhanceEffect();
    }

    public void PlayChickensRunAnimation()
    {
        foreach (Chicken i in _createdChickens)
        {
            i.GetComponent<ChickenAnimator>().OnPlayRunAnimation();
        }
    }

    public void PlayChickensIdleAnimation()
    {
        foreach (Chicken i in _createdChickens)
        {
            i.GetComponent<ChickenAnimator>().OnPlayIdleAnimation();
        }
    }

    public void OnMoveChickenScreenOut()
    {
        foreach(Chicken i in _createdChickens)
        {
            i.transform.DOMoveX(i.transform.position.x + PositionToMoveScreenOut, DuractionToMoveScreenOut).SetEase(Ease.Linear);
        }
    }

    public void RemoveChicken(Chicken chicken)
    {
        _createdChickens.Remove(chicken);

        ChickenDied?.Invoke(chicken.ChickenParams.Index);

        if (ChickensAmount <= 0)
        {
            EnemiesSpawner.Instance.CheckIfBossWin();
            CreateChicken();
        }
    }
    
    public Chicken GetFirstChicken()
    {
        return _createdChickens[0];
    }

    public Chicken[] GetAllChickens()
    {
        return _createdChickens.ToArray();
    }

    public void OnMoveChickenInitPos()
    {
        foreach (Chicken i in _createdChickens)
        {
            i.transform.position = new Vector3(-PositionToMoveScreenOut, i.transform.position.y, 0f);
            i.transform.DOMoveX(_chickenPositions[_selectedChickensByPointsParameter.GetIndexByChicken(i)].transform.position.x, DuractionToMoveScreenOut).SetEase(Ease.Linear);
        }
    }
}
