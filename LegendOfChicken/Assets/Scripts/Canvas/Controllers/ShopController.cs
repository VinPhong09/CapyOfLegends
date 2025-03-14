using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Image[] _allButtonPanels;
    [SerializeField] private Sprite _selectedImg;
    [SerializeField] private Sprite _nonSelected;
    
    public void OnSelectPanel(Image selected)
    {
        foreach (Image i in _allButtonPanels)
        {
            i.sprite = _nonSelected;
        }
        selected.sprite = _selectedImg;
    }

    /*public void OpenPanel(GameObject panel)
    {

    }*/
}
