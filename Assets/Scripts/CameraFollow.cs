using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform _target; //место гусеницы
    [SerializeField] private float _smoothSpeed = 0.125f; //сглаживание
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10); // смещение камеры
    [SerializeField] private float _minX; // граница слева
    [SerializeField] private float _maxX; // граница справа
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;

        float clampedX = Mathf.Clamp(desiredPosition.x, _minX, _maxX);
        Vector3 limitedPosition = new Vector3(clampedX, desiredPosition.y, desiredPosition.z);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, limitedPosition, _smoothSpeed);

        transform.position = smoothPosition;
    }
}
