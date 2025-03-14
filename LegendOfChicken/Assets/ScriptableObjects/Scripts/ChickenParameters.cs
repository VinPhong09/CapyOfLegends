using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Characters/Chicken")]
public class ChickenParameters : CharacterParameters
{
    [SerializeField] private int _index;
    [SerializeField] private int _level;
    [SerializeField] private bool _isLocked;
    [SerializeField] private Sprite _icon;
    [SerializeField] private RenderTexture _chickenRenderView;

    public ActiveSkillsParameters[] ActiveSkills;

    public Sprite Icon => _icon;

    public int Index => _index;

    public RenderTexture ChickenRenderView => _chickenRenderView;

    public bool IsLocked
    {
        get
        {
            return _isLocked;
        }
        set
        {
            _isLocked = value;
        }
    }

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
}
