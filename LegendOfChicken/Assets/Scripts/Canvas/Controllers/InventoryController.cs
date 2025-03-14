using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _leveingTab;
    [SerializeField] private GameObject _equipmentElementTab;
    [SerializeField] private Image[] _panelButtons;

    [Header("Equipment Elements")]
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _elementIcon;

    private readonly float NoneSelectPanelButtonPosY = 1677f;
    private readonly float SelectPanelButtonPosY = 1710f;
    private readonly Color32 SelectPanelColor = new Color(.5f, .5f, .5f, 1f);

    private EquipmentElement _selectedElement;

    public void ResetPanelButtons()
    {
        foreach (Image i in _panelButtons)
        {
            i.color = Color.white;
            //i.rectTransform.position = new Vector2(i.transform.position.x, NoneSelectPanelButtonPosY);
        }
    }

    public void SetPanelButton(Image img)
    {
        ResetPanelButtons();
        img.color = SelectPanelColor;
        //img.rectTransform.position = new Vector2(img.transform.position.x, SelectPanelButtonPosY);
        img.gameObject.SetActive(true);
    }

    public void SelectEquipmentElement(EquipmentElement equipmentElement)
    {
        _selectedElement = equipmentElement;

        _label.text = equipmentElement.EquipmentElementParameters.Label;
        _elementIcon.sprite = equipmentElement.EquipmentElementParameters.Icon;
    }
}
