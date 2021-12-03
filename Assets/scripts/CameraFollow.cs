using UnityEngine;
using Photon.Pun;

namespace Com.Jervw.Crimson
{
    public class CameraFollow : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        public float smoothSpeed = 0.125f;

        [SerializeField]
        public Vector3 cameraOffset;

        [SerializeField]
        SpriteRenderer groundSprite;

        [SerializeField]
        Transform target;

        [SerializeField]
        Vector3 minBounds, maxBounds;

        [SerializeField]
        float halfHeight, halfWidth;

        Camera cam;


        void Start()
        {
            if (photonView.IsMine)
            {
                cam = Camera.main;
                groundSprite = GameObject.Find("Ground").GetComponent<SpriteRenderer>();

                minBounds = groundSprite.bounds.min;
                maxBounds = groundSprite.bounds.max;
                halfHeight = cam.orthographicSize;
                halfWidth = halfHeight * Screen.width / Screen.height;
            }
        }

        void LateUpdate()
        {
            if (photonView.IsMine)
            {
                Vector3 desiredPosition = transform.position + cameraOffset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                cam.transform.position = smoothedPosition;

                cam.transform.position = new Vector3(
                    Mathf.Clamp(cam.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth),
                    Mathf.Clamp(cam.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight),
                    cam.transform.position.z
                );
            }
        }
    }
}