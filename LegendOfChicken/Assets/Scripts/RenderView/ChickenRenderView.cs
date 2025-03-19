using UnityEngine;

public class ChickenRenderView : RenderView
{
    [SerializeField] private ChickenEquipmentParameters _chickenEquipment;
    [SerializeField] private SpriteRenderer _weapon;
    [SerializeField] private SpriteRenderer _hat;
    [SerializeField] private SpriteRenderer _shield;

    private void Awake()
    {
        if (RenderViewController.Instance == null)
            return;

        RenderViewController.Instance.AddAction(() => Renderer());
    }

    public override void Renderer()
    {
        if (_chickenEquipment == null)
        {
            return;
            _weapon.gameObject.SetActive(false);
            _hat.gameObject.SetActive(false);
            _shield.gameObject.SetActive(false);
            
        }

        _weapon.gameObject.SetActive(true);
        _hat.gameObject.SetActive(true);
        _shield.gameObject.SetActive(true);

        if(_chickenEquipment.WeaponIcon != null)
            _weapon.sprite = _chickenEquipment.WeaponIcon.Icon;
        else
            _weapon.gameObject.SetActive(false);

        if (_chickenEquipment.HatIcon != null)
            _hat.sprite = _chickenEquipment.HatIcon.Icon;
        else
            _hat.gameObject.SetActive(false);

        if (_chickenEquipment.ArmorIcon != null)
            _shield.sprite = _chickenEquipment.ArmorIcon.Icon;
        else
            _shield.gameObject.SetActive(false);
    }
}
