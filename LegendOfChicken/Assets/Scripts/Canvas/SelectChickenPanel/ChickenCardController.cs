using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChickenCardController : MonoBehaviour
{
    public static ChickenCardController Instance;

    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _cancelButton;
    [SerializeField] private ChickensPlatformElement[] _chickensPlatforms;
    [SerializeField] private List<ChickenCardElement> _cardsContainer = new List<ChickenCardElement>();
    [SerializeField] private SelectedChickensByPointsParameter _selectedChickensByPointsParameter;

    private readonly float MoveDuraction = 0.2f;

    public ChickenCardElement SelectedChickenCard { get; set; }

    private void OnEnable()
    {
        Instance = this;

        CheckSelected();
    }

    public void SelectCard(ChickenCardElement chickenCardElement)
    {
        int index = _cardsContainer.IndexOf(chickenCardElement);

        HideAllCards();
        ActivateArrows();

        _gridLayoutGroup.enabled = false;
        _scrollRect.enabled = false;

        _cardsContainer[index].gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence()
            .Append(_cardsContainer[index].GetComponent<RectTransform>().DOMove(new Vector3(730f, 1300f, 0f), MoveDuraction))
            .AppendCallback(() => _cancelButton.SetActive(true));
    }

    public void CheckSelected()
    {
        foreach(ChickenCardElement i in _cardsContainer)
        {
            if (!_selectedChickensByPointsParameter.TryFindSelectedChickenByIndex(i.Chicken.ChickenParams.Index))
            {
                i.OnActivateSelect();
                continue;
            }

            i.HideSelect();
        }
    }

    public void ActivateArrows()
    {
        foreach (ChickensPlatformElement i in _chickensPlatforms)
        {
            i.ActivateArrow();
        }
    }

    public void DisactivateArrows()
    {
        foreach (ChickensPlatformElement i in _chickensPlatforms)
        {
            i.DisactivateArrow();
        }
    }

    public void ResetAllCard()
    {
        _gridLayoutGroup.enabled = true;
        _scrollRect.enabled = true;
        _cancelButton.SetActive(false);

        DisactivateArrows();
        ActivateAllCard();
    }

    public void HideAllCards()
    {
        foreach (ChickenCardElement i in _cardsContainer)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void HideAllButtonsOnCards()
    {
        foreach (ChickenCardElement i in _cardsContainer)
        {
            i.HideButtons();
        }
    }

    public ChickenCardElement FindCardByIndex(int chickenIndex)
    {
        foreach (ChickenCardElement i in _cardsContainer)
        {
            if(i.Chicken.ChickenParams.Index == chickenIndex)
            {
                return i;
            }
        }

        return null;
    }

    public void ActivateAllCard()
    {
        foreach (ChickenCardElement i in _cardsContainer)
        {
            i.gameObject.SetActive(true);
        }

        DisactivateArrows();

        SelectedChickenCard = null;
    }

}
