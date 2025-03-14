using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Characters/Boss")]
public class BossParameters : CharacterParameters
{
    public Sprite Icon;
    public RenderTexture BossRenderView;
}
