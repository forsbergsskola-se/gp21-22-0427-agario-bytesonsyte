using System.Collections;
using Agario.Player;
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
        private GameObject FoodHolder;
        public int foodCount;
        public GameObject Player;
        public float RespawnTime;
        private bool onlyOnce = true;



        Vector2 cubeSize;
        Vector2 cubeCenter;

        private void Awake()
        {
            Cam = Camera.main;
            SpawnLevel();
            CalculateSpawnRange();
            SpawnPlayer();
            FoodHolder = new GameObject("Food Holder");
            for (int i = 0; i < MaxFoodCount; i++)
                SpawnFood();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                var playerScore = FindObjectOfType<PlayerScore>();
                playerScore.Score = 0;
                playerScore.ScoreUI.text = "Score: 0 ";
                Destroy(Player);
            }
            
            if (Player == null && onlyOnce)
            {
                Debug.Log("Respawning...");
                StartCoroutine(RespawnUponDeath());
            }
            
            foodCount = FoodHolder.transform.childCount;
            if (foodCount >= MaxFoodCount) return;
            if (foodCount <= MaxFoodCount)
                SpawnFood();
        }

        private void SpawnLevel()
        {
            var camScale = new Vector3(Cam!.pixelWidth, Cam!.pixelHeight, 0);
            Instantiate(backgroundPrefab, Vector3.zero, quaternion.identity);
            backgroundPrefab.transform.localScale = camScale * ScalingMultiplier;
        }

        public void SpawnPlayer()
        {
            var spawnPoint = RandomPos();
            Instantiate(playerPrefab, spawnPoint, quaternion.identity);
            Player = GameObject.FindWithTag("Player");
            Debug.Log($"{Player.gameObject.name} spawned at: {spawnPoint}");
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
            Instantiate(foodPrefab, RandomPos(), Quaternion.identity, FoodHolder.transform);
        }

        private Vector3 RandomPos()
        {
            Vector2 randomSpawn = new Vector3(Random.Range(-cubeSize.x / 2, cubeSize.x / 2),
                Random.Range(-cubeSize.y / 2, cubeSize.y / 2), 0);
            return randomSpawn + cubeCenter;
        }
        
        private IEnumerator RespawnUponDeath()
        {
            onlyOnce = false;
            yield return new WaitForSeconds(RespawnTime);
            Debug.Log("Spawning Player");
            SpawnPlayer();
            Player = GameObject.FindWithTag("Player");
            Cam.GetComponent<CameraFollow>().UpdatePlayerComponents();
            onlyOnce = true;
        }
    }
}
