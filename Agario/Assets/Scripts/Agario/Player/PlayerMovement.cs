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

        public GameObject field;
        private float xMin, xMax, yMin, yMax;

        private void Start()
        {
            mainCam = Camera.main;
            currentSpeed = maxSpeed;
            startScale = transform.localScale.x;
            CalculateMapBounds();
        }

        private void FixedUpdate()
        {
            var mouseLocation = mainCam!.ScreenToWorldPoint(Input.mousePosition);
            TargetPos = new Vector3(Mathf.Clamp(mouseLocation.x, xMin, xMax),
                Mathf.Clamp(mouseLocation.y, yMin, yMax), 0); // clamps targetPos so cannot go outside map bounds
        }


        private void Update()
        {
            if (currentSpeed == minSpeed)
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
        
        
        private void CalculateMapBounds()
        {
            var fieldTransform = field.transform.position;
            var fieldScale = field.transform.localScale;
            xMin = fieldTransform.x - (fieldScale.x / 2);
            xMax = fieldTransform.x + (fieldScale.x / 2);
            
            yMin = fieldTransform.y - (fieldScale.y / 2);
            yMax = fieldTransform.y + (fieldScale.y / 2);
            //Debug.Log($"MinX = {xMin}, MaxX = {xMax}, MinY = {yMin}, MaxY = {yMax}");
        }
    }
}
