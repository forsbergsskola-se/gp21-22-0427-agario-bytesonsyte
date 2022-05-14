using UnityEngine;

namespace Agario.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float maxSpeed;
        [SerializeField] public float minSpeed;
        public float currentSpeed;
        private float startScale;
        private float scaleAmount = 0;
        public float speedMultiplier;

        private Vector3 TargetPos;
        private Vector3 CurrentPos;
        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
            currentSpeed = maxSpeed;
            startScale = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            TargetPos = mainCam!.ScreenToWorldPoint(Input
                .mousePosition); // gets targetPos from mouse location (with a null check)
        }


        private void Update()
        {
            if (currentSpeed != minSpeed)
                CalculateSpeed();
            
            CurrentPos = transform.position;
            TargetPos.z = CurrentPos.z;
            
            var scale = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f); // makes deltaTime independent of framerate
            gameObject.transform.position = Vector3.MoveTowards(CurrentPos, TargetPos, // constantly move towards mouse (TargetPos)
                (float) currentSpeed * scale); // use deltaTime to negate computer performance effecting PlayerSpeed
            // transform.localScale.x);  // the higher the player's scale, the lower their speed
        }
        
        private void CalculateSpeed()
        {
            var currentScale = transform.localScale.x;
            
            if (currentScale > startScale)
                scaleAmount = currentScale - startScale;
            else
                return;

            if (currentSpeed > minSpeed)
                currentSpeed = maxSpeed - (scaleAmount / speedMultiplier);
            else
                currentSpeed = minSpeed;
        }
    }
}
