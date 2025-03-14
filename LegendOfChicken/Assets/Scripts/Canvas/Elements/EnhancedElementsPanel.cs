using System.Collections.Generic;
using UnityEngine;

public class EnhancedElementsPanel : MonoBehaviour
{
    [SerializeField] private EnhancedElement _enhancedElement;
    [SerializeField] private RectTransform _container;

    private readonly float DelayTime = 0.035f;

    private Coroutine _appearElements;

    private List<EnhancedElement> _enhancedElements = new List<EnhancedElement>();

    private void OnDisable()
    {
        foreach (EnhancedElement i in _enhancedElements)
        {
            Destroy(i.gameObject);
        }
        _enhancedElements.Clear();
    }

    public void Initialize(params ChickenEquipmentElementParameters[] chickenEquipmentElementParameters)
    {
        if (chickenEquipmentElementParameters.Length <= 0)
            return;

        for (int i = 0; i < chickenEquipmentElementParameters.Length; i++)
        {
            EnhancedElement enhancedElement = Instantiate(_enhancedElement);
            enhancedElement.transform.SetParent(_container);
            enhancedElement.Initialize(chickenEquipmentElementParameters[i]);

            _enhancedElements.Add(enhancedElement);
        }
    }
}
