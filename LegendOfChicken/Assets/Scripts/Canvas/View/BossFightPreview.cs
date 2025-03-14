using System;
using UnityEngine;
using UnityEngine.UI;

public class BossFightPreview : MonoBehaviour
{
    public event Action PreviewCompleted;

    [SerializeField] private RawImage _enemyImg;
    [SerializeField] private RawImage[] _chickenImgs;
    [SerializeField] private SelectedChickensByPointsParameter _selectedChickensByPointsParameter;

    public void Initialize(Texture enemy, Action completeAction)
    {
        _enemyImg.texture = enemy;
        
        for(int i = 0; i < _chickenImgs.Length; i++)
        {
            if(_selectedChickensByPointsParameter.GetChickenByIndex(i) == null)
            {
                _chickenImgs[i].gameObject.SetActive(false);
                continue;
            }

            _chickenImgs[i].texture = _selectedChickensByPointsParameter.GetChickenByIndex(i).ChickenParams.ChickenRenderView;
        }

        PreviewCompleted += completeAction;

        gameObject.SetActive(true);
    }

    public void OnPreviewCompleted()
    {
        PreviewCompleted?.Invoke();
    }
}
