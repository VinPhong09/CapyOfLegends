using UnityEngine;

public class TillingBackground : MonoBehaviour
{
    [SerializeField] private Material _backgroundMaterial;

    [SerializeField] private Vector2 _duraction;

    private Vector2 _currentPos = Vector2.zero;

    private void Awake()
    {
        _backgroundMaterial.mainTextureOffset = new Vector2(0f, 0f);
    }

    private void Update()
    {
        _currentPos += _duraction * Time.deltaTime;

        if (_currentPos.x >= 1f)
            _currentPos.x = 0f;

        if (_currentPos.y >= 1f)
            _currentPos.y = 0f;

        _backgroundMaterial.mainTextureOffset = _currentPos;
    }
}
