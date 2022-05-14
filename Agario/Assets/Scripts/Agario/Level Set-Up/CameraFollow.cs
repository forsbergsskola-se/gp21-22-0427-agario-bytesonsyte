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

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();
            playerSpeed = player.GetComponent<PlayerMovement>().Speed;
        }
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            followTransform = player.transform.position;
            playerVelocity = playerRb.velocity;
            var smoothedPos = Vector3.SmoothDamp(gameObject.transform.position, followTransform, ref playerVelocity, (float) playerSpeed* Time.smoothDeltaTime);
            gameObject.transform.position = new Vector3(smoothedPos.x, smoothedPos.y, -1);
        }
    }
}