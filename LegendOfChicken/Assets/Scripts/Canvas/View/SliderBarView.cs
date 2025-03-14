using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderBarView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [Header("Smooth Parameters")]
    [SerializeField] private float _duraction;

    public void ChangeSliderValue(float maxValue, float current)
    {
        float onePer = 1f / maxValue;

        _slider.value = onePer * current;
    }

    public void SmoothValueChange(float value)
    {
        _slider.DOValue(value, _duraction);
    }
}
