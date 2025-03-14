using System;
using UnityEngine;
using UnityEngine.Events;

public class RenderViewController : MonoBehaviour
{
    public static RenderViewController Instance;

    private UnityEvent RenderedAllViews = new UnityEvent();

    [SerializeField] private RenderView[] _renderViewChildren;

    public void Initialize()
    {
        Instance = this;
    }

    public void AddAction(UnityAction action)
    {
        RenderedAllViews.AddListener(action);
    }

    public void Invoke()
    {
        RenderedAllViews?.Invoke();

        foreach (RenderView i in _renderViewChildren)
        {
            i.Renderer();
        }
    }
}
