using System.Collections;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Material _backgroundMaterial;

    [SerializeField] private float _speed;
    [SerializeField] private float _parallaxEffect;

    private Coroutine _coroutine;

    private float _currentPos = 0f;

    private void Awake()
    {
        _backgroundMaterial.mainTextureOffset = new Vector2(0f, 0f);
    }

    public void StartBackgroundMove()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(nameof(MoveBackground));
    }

    public void StopBackgroundMove()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);

        _coroutine = null;
    }

    private IEnumerator MoveBackground()
    {
        var waitForUpdate = new WaitForEndOfFrame();

        while (true)
        {
            _currentPos += _speed * _parallaxEffect * Time.deltaTime;

            if (_currentPos >= 1f)
                _currentPos = 0f;

            _backgroundMaterial.mainTextureOffset = new Vector2(_currentPos, 0f);

            yield return waitForUpdate;
        }
    }
}
