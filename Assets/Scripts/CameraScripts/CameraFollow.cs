using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f;
    public Vector3 offset; 
    public float minX, maxX, minY, maxY;

    public GameObject triggerBox;

    private bool followEnabled = false;

    private void LateUpdate()
    {
        if (followEnabled)
        {
            Vector3 maxPosition = target.position + offset;

            float clampedX = Mathf.Clamp(maxPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(maxPosition.y, minY, maxY);
            maxPosition = new Vector3(clampedX, clampedY, maxPosition.z);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, maxPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject == triggerBox)
        {
            followEnabled = true;
        }
    }
}
