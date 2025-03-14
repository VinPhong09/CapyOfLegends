using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollEnhancesElement : MonoBehaviour
{
    public event Action DisableAction;

    [SerializeField] private Transform _elementsContainer;
    [SerializeField] private ChickenParameters _chickenParameters;

    private int _index;

    public int Index => _index;

    public void Initialize(Transform parent, int index, Action disableAction)
    {
        transform.SetParent(parent);
        
        RectTransform offset = GetComponent<RectTransform>();
        offset.offsetMin = Vector2.zero;
        offset.offsetMax = Vector2.zero;

        transform.localScale = Vector3.one;

        _index = index;

        for(int i = 0; i < _elementsContainer.childCount; i++)
        {
            _elementsContainer.GetChild(i).TryGetComponent(out EnhanceElement enhanceElement);
            if(enhanceElement != null)
                enhanceElement.Initialize(_chickenParameters);
        }

        DisableAction += disableAction;
    }

    private void OnDisable()
    {
        DisableAction?.Invoke();
    }
}
