using UnityEngine;
using UnityEngine.Events;

public class AnimatorBasicActions : MonoBehaviour
{
    [SerializeField] private UnityEvent AnimationCompleted;

    public void DestroyCurrentObject()
    {
        Destroy(gameObject);
    }

    public void DestroyObjParent()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void AddAnimationCompleteAction(UnityAction action)
    {
        AnimationCompleted.AddListener(action);
    }

    public void OnAnimationCompleted()
    {
        AnimationCompleted?.Invoke();
    }

    public void DisactivateCurrentObject()
    {
        gameObject.SetActive(false);
    }
}
