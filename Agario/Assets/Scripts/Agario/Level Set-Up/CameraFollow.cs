using Agario.Player;
using UnityEngine;

namespace Agario.Level_Set_Up
{
    public class CameraFollow : MonoBehaviour
    {
        private Vector3 followTransform;
        public BoxCollider2D mapBounds;

        private float xMin, xMax, yMin, yMax;
        private float camY,camX;
        private float camOrthSize;
        private float cameraRatio;
        private Camera mainCam;
        public PlayerMovement Player;

        private void Start()
        {
            Player = FindObjectOfType<PlayerMovement>();
            var bounds = mapBounds.bounds;
            xMin = bounds.min.x;
            xMax = bounds.max.x;
            yMin = bounds.min.y;
            yMax = bounds.max.y;

            camOrthSize = GetComponent<Camera>().orthographicSize;
            cameraRatio = (xMax + camOrthSize) / 2.0f;
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            followTransform = Player.gameObject.transform.position;
            camY = Mathf.Clamp(followTransform.y, yMin + camOrthSize, yMax - camOrthSize);
            camX = Mathf.Clamp(followTransform.x, xMin + cameraRatio, xMax - cameraRatio);
            //transform.position = new Vector3(followTransform.x, followTransform.y, -1);
        }
    }
}