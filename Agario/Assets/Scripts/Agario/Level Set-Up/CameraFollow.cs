using Agario.Player;
using UnityEngine;

namespace Agario.Level_Set_Up
{
    public class CameraFollow : MonoBehaviour
    {
        private Vector3 followTransform;
        private GameObject player;
        private Rigidbody2D playerRb;
        private Vector3 playerVelocity;
        private float playerSpeed;
        private Vector3 playerPos;

        private void Start()
        {
            UpdatePlayerComponents();
        }

        public void UpdatePlayerComponents()
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();
            playerSpeed = player.GetComponent<PlayerMovement>().currentSpeed;
        }
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            if (player.gameObject != null)
            {
                playerPos = player.transform.position;
                followTransform = playerPos;
                playerVelocity = playerRb.velocity;
                var smoothedPos = Vector3.SmoothDamp(gameObject.transform.position, followTransform, ref playerVelocity,
                    (float) playerSpeed * Time.smoothDeltaTime);
                gameObject.transform.position = new Vector3(smoothedPos.x, smoothedPos.y, -1);
            }

            else
            {
                gameObject.transform.position = transform.position;
            }
        }
    }
}