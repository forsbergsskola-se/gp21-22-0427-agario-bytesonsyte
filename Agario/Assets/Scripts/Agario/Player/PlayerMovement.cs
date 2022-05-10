using System;
using UnityEngine;

namespace Agario.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float Speed;
        private Vector3 TargetPos;
        private Vector3 CurrentPos;
        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        public void FixedUpdate()
        {
            CurrentPos = transform.position;
            TargetPos = mainCam!.ScreenToWorldPoint(Input.mousePosition); // gets targetPos from mouse location (with a null check)
        }

        private void Update()
        {
            TargetPos.z = CurrentPos.z;
            transform.position = Vector3.MoveTowards(CurrentPos, TargetPos, // constantly move towards mouse (TargetPos)
                Speed * Time.deltaTime);  // use deltaTime to negate computer performance effecting PlayerSpeed
        }
    }
}
