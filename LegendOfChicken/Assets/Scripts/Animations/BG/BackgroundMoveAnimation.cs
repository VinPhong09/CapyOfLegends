using UnityEngine;

public class BackgroundMoveAnimation : MonoBehaviour
{
    [SerializeField] private BackGround[] _backGrounds;

    public void StartMove()
    {
        foreach (BackGround i in _backGrounds)
            i.StartBackgroundMove();
    }

    public void StopMove()
    {
        foreach (BackGround i in _backGrounds)
            i.StopBackgroundMove();
    }
}
