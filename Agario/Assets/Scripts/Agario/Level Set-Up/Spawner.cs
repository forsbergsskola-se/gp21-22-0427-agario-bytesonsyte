using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agario.Level_Set_Up
{
    public class Spawner : MonoBehaviour
    {
        public float ScalingMultiplier;
        public GameObject backgroundPrefab;
        public GameObject playerPrefab;
        public GameObject foodPrefab;
        public BoxCollider2D spawnZone;
        private Camera Cam;
        public int MaxFoodCount;
        
        
        Vector2 cubeSize;
        Vector2 cubeCenter;

        private void Awake()
        {
            Cam = Camera.main;
            SpawnLevel();
            SpawnPlayer();
            CalculateSpawnRange();
            
            for (int i = 0; i <= MaxFoodCount; i++)
                SpawnFood();
        }

        private void SpawnLevel()
        {
            var camScale = new Vector3(Cam!.pixelWidth, Cam!.pixelHeight, 0);
            Instantiate(backgroundPrefab, Vector3.zero, quaternion.identity);
            backgroundPrefab.transform.localScale = camScale * ScalingMultiplier;
        }

        private void SpawnPlayer()
        {
            Instantiate(playerPrefab, Vector3.zero, quaternion.identity);
        }

        private void CalculateSpawnRange()
        {
            Transform cubeTrans = spawnZone.GetComponent<Transform>();
            cubeCenter = spawnZone.GetComponent<Transform>().position;
            
            cubeSize.x = cubeTrans.localScale.x * spawnZone.size.x;
            cubeSize.y = cubeTrans.localScale.y * spawnZone.size.y;
        }

        private void SpawnFood()
        {
            Vector2 randomPosition = new Vector3(Random.Range(-cubeSize.x/ 2, cubeSize.x/ 2), Random.Range(-cubeSize.y/ 2, cubeSize.y/ 2), 0);
            Instantiate(foodPrefab, randomPosition + cubeCenter, Quaternion.identity);
        }
    }
}
