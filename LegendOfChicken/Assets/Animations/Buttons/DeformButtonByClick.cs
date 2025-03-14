using UnityEngine;

public class DeformButtonByClick : MonoBehaviour
{
    public void OnPointerDown()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    public void OnPointerUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
