using UnityEngine;
using DG.Tweening;

public class CoinCollectedImage : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _duraction;
    [SerializeField] private AudioSource _audioSource;

    private Tween _tween;

    public void Initialize(Vector3 initPos, Vector3 endPos, int creditAmount)
    {
        _rectTransform.position = initPos;

        float moveDuraction = Random.Range(0.5f, 1.0f);

        _tween = DOTween.Sequence()
            .Append(_rectTransform.DOJump(new Vector3(initPos.x + Random.Range(-150f, 150f), initPos.y, initPos.z), 80f, 2, moveDuraction))
            .Append(_rectTransform.DOMove(endPos, moveDuraction).SetEase(Ease.InOutBack, moveDuraction))
            .AppendCallback(() => _audioSource.Play())
            .Append(_rectTransform.DOMove(endPos, 0.2f))
            .AppendCallback(() => CurrencyView.Instance.CreditCoins(creditAmount))
            .AppendCallback(() => Destroy(gameObject));
    }
}
