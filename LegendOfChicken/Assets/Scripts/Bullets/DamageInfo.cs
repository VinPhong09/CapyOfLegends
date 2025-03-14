using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageInfo : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _duraction;
    [SerializeField] private float _offset;

    public void Initialize(int value, Color color, Vector3 initPos)
    {
        Vector3 endPos = initPos + new Vector3(0f, _offset, 0f);
        _rectTransform.position = initPos;

        _text.color = color;
        _text.text = value.ToString();

        Sequence sequence = DOTween.Sequence()
            .Append(_rectTransform.DOMoveY(endPos.y, _duraction))
            .Append(_text.DOFade(0f, _duraction))
            .AppendCallback(() => Destroy(gameObject));
    }
}
