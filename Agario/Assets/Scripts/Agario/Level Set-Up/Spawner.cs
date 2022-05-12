using System;
using Unity.Mathematics;
using UnityEngine;

namespace Agario.Level_Set_Up
{
    public class Spawner : MonoBehaviour
    {
        public GameObject backgroundPrefab;
        public float ScalingMultiplier;
        public GameObject playerPrefab;
        private Camera Cam;

        private void Awake()
        {
            Cam = Camera.main;
            SpawnLevel();
        }

        private void SpawnLevel()
        {
            var camScale = new Vector3(Cam!.pixelWidth, Cam!.pixelHeight, 0);
            Instantiate(backgroundPrefab, Vector3.zero, quaternion.identity);
            backgroundPrefab.transform.localScale = camScale * ScalingMultiplier;
        }
    }
}
