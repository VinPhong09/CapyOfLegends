using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChickenEquipment : MonoBehaviour
{
    [SerializeField] private ChickenEquipmentParameters _chickenEquipmentParameters;

    [Header("Top Section")]
    public TextMeshProUGUI NameText;
    public Image WeaponIconImgDefualt;
    public Image HatIconImgDefualt;
    public Image ArmorIconImgDefualt;
    public Image ExtraAttributeIconImgDefualt;
    public Image WeaponIconImg;
    public Image HatIconImg;
    public Image ArmorIconImg;
    public Image ExtraAttributeIconImg;

    [Header("Middle Section")]
    [Header("Effects")]
    public Image EffectIcon1Img;
    public Image EffectIcon2Img;
    public Image EffectIcon3Img;
    public Image EffectIcon4Img;
    public Image EffectIcon5Img;

    [Header("Passive Skills")]
    public Image PassiveSkillsIconImg;
    public TextMeshProUGUI PassiveSkillsDescriptionText;

    public ChickenEquipmentParameters ChickenEquipmentParameters => _chickenEquipmentParameters;


    public void Initialize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.SetParent(ChickenCardController.Instance.transform.parent.transform);

        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;

        transform.localScale = Vector3.one;

        UpdateInfo();
    }

    private void Update()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        NameText.text = _chickenEquipmentParameters.CharacterParameters.Name;

        if (_chickenEquipmentParameters.WeaponIcon == null)
        {
            WeaponIconImgDefualt.gameObject.SetActive(true);
            WeaponIconImg.gameObject.SetActive(false);
        }
        else
        {
            WeaponIconImgDefualt.gameObject.SetActive(false);
            WeaponIconImg.sprite = _chickenEquipmentParameters.WeaponIcon.Icon;
            WeaponIconImg.gameObject.SetActive(true);
        }

        if (_chickenEquipmentParameters.HatIcon == null)
        {
            HatIconImgDefualt.gameObject.SetActive(true);
            HatIconImg.gameObject.SetActive(false);
        }
        else
        {
            HatIconImgDefualt.gameObject.SetActive(false);
            HatIconImg.sprite = _chickenEquipmentParameters.HatIcon.Icon;
            HatIconImg.gameObject.SetActive(true);
        }

        if (_chickenEquipmentParameters.ArmorIcon == null)
        {
            ArmorIconImgDefualt.gameObject.SetActive(true);
            ArmorIconImg.gameObject.SetActive(false);
        }
        else
        {
            ArmorIconImgDefualt.gameObject.SetActive(false);
            ArmorIconImg.sprite = _chickenEquipmentParameters.ArmorIcon.Icon;
            ArmorIconImg.gameObject.SetActive(true);
        }

        if (_chickenEquipmentParameters.ExtraAttributeIcon == null)
        {
            ExtraAttributeIconImgDefualt.gameObject.SetActive(true);
            ExtraAttributeIconImg.gameObject.SetActive(false);
        }
        else
        {
            ExtraAttributeIconImgDefualt.gameObject.SetActive(false);
            ExtraAttributeIconImg.sprite = _chickenEquipmentParameters.ExtraAttributeIcon;
            ExtraAttributeIconImg.gameObject.SetActive(true);
        }

        EffectIcon1Img.sprite = _chickenEquipmentParameters.EffectIcon1;
        EffectIcon2Img.sprite = _chickenEquipmentParameters.EffectIcon2;
        EffectIcon3Img.sprite = _chickenEquipmentParameters.EffectIcon3;
        EffectIcon4Img.sprite = _chickenEquipmentParameters.EffectIcon4;
        EffectIcon5Img.sprite = _chickenEquipmentParameters.EffectIcon5;

        PassiveSkillsIconImg.sprite = _chickenEquipmentParameters.PassiveSkillsIcon;
        PassiveSkillsDescriptionText.text = _chickenEquipmentParameters.PassiveSkillsDescription;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
