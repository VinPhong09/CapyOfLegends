using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Enhance Element", menuName = "ScriptableObjects/Enhance Element/Element")]
public class EnhanceElementParameters : ScriptableObject
{
    [SerializeField] private UnityEvent Enhanced;

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private int _level;
    [SerializeField] private float _value;
    public float _cost;
    public float _baseCost;

    public Sprite Icon => _icon;
    public string Label => _label;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }
    
    public float BaseCost
    {
        get
        {
            return _baseCost;
        }
        set
        {
            _baseCost = value;
        }
    }

    public float Cost{
        get{
            return _cost;
        }
        set{
            _cost = value;
        }
    }

    public void OnEnhance()
    {
        Enhanced?.Invoke();
    }

}
