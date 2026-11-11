using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 offset;
    private Vector3 velocity;

    private void Start()
    {
        // Keeps whatever distance/angle you set up in the Scene view
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}