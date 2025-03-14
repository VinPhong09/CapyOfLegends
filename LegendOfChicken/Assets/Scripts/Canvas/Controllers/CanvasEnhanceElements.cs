using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnhanceElements : MonoBehaviour
{
    [SerializeField] private List<ScrollEnhancesElement> _scrollEnhancesElements = new List<ScrollEnhancesElement>();

    public void CreateOrActivateScrollEnhancesElement(ScrollEnhancesElement scrollEnhancesElement, ChickenParameters chickenParameters, Action disableAction)
    {
        void ActivateCurrentScrollPanel()
        {
            for (int i = 0; i < _scrollEnhancesElements.Count; i++)
            {
                if (chickenParameters.Index == _scrollEnhancesElements[i].Index)
                {
                    _scrollEnhancesElements[i].gameObject.SetActive(true);
                    continue;
                }

                _scrollEnhancesElements[i].gameObject.SetActive(false);
            }
        }

        if (TryAddNewScrollEnchancesElement(chickenParameters.Index))
        {
            ScrollEnhancesElement enhancesElement = Instantiate(scrollEnhancesElement);
            enhancesElement.Initialize(transform, chickenParameters.Index, disableAction);
            _scrollEnhancesElements.Add(enhancesElement);

            ActivateCurrentScrollPanel();
            return;
        }

        ActivateCurrentScrollPanel();
    }

    private bool TryAddNewScrollEnchancesElement(int index)
    {
        if (_scrollEnhancesElements.Count <= 0)
            return true;

        foreach (ScrollEnhancesElement i in _scrollEnhancesElements)
        {
            if (i.Index == index)
                return false;
        }

        return true;
    }

    public void DestroyScrollElementByIndex(int index)
    {
        for (int i = 0; i < _scrollEnhancesElements.Count; i++)
        {
            if (index == _scrollEnhancesElements[i].Index)
            {
                Destroy(_scrollEnhancesElements[i].gameObject);
                _scrollEnhancesElements.Remove(_scrollEnhancesElements[i]);
                break;
            }
        }
    }

    public void HideAllScrollEnhances()
    {
        foreach(ScrollEnhancesElement i in _scrollEnhancesElements)
        {
            Destroy(i.gameObject);
        }
        _scrollEnhancesElements.Clear();
    }
}
