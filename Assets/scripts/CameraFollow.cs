using UnityEngine;

namespace Com.Jervw.Crimson
{
    public class CameraFollow : MonoBehaviour
    {
        const float SMOOTH_AMOUNT = 0.125f;

        [SerializeField] private Vector3 cameraOffset;

        Camera cam;
        Vector3 minBounds, maxBounds;
        float halfHeight, halfWidth;

        void Start()
        {

            cam = Camera.main;
            minBounds = LevelHandler.Instance.current.minBounds;
            maxBounds = LevelHandler.Instance.current.maxBounds;
            halfHeight = cam.orthographicSize;
            halfWidth = halfHeight * Screen.width / Screen.height;
        }

        void LateUpdate()
        {
            var desiredPosition = transform.position + cameraOffset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SMOOTH_AMOUNT);
            cam.transform.position = smoothedPosition;

            cam.transform.position = new Vector3(
                Mathf.Clamp(cam.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth),
                Mathf.Clamp(cam.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight),
                cam.transform.position.z
            );
        }
    }
}