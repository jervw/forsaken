using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public float smoothSpeed = 0.125f;

    [SerializeField]
    public Vector3 cameraOffset;

    public SpriteRenderer groundSprite;

    private Transform target;
    private Camera cam;
    private Vector3 minBounds, maxBounds;
    private float halfHeight, halfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();

        target = GameObject.FindWithTag("Player").transform;

        minBounds = groundSprite.bounds.min;
        maxBounds = groundSprite.bounds.max;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + cameraOffset;
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
