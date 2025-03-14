using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateDiraction;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Rotate(_rotateDiraction * _speed);
    }
}
