using System.Collections;
using Agario.Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agario.Level_Set_Up
{
    public class Spawner : MonoBehaviour
    {
        public float FieldSizeMultiplier;
        public GameObject backgroundPrefab;
        public GameObject playerPrefab;
        public GameObject foodPrefab;
        public BoxCollider2D spawnZone;
        private Camera Cam;
        public int MaxFoodCount;
        private GameObject FoodHolder;
        public int foodCount;
        public GameObject Player;
        private string playerName;
        public float RespawnTime;
        private bool onlyOnce = true;

        private Vector2 fieldSize;
        private Vector2 fieldCenter;

        
        private void Awake()
        {
            Cam = Camera.main;
            SpawnLevel();
            CalculateSpawnRange();
            playerName = PlayerPrefs.GetString("Player Name");
            SpawnPlayer();
            FoodHolder = new GameObject("Food Holder");
            
            for (var i = 0; i < MaxFoodCount; i++)
                SpawnOrbsFood();
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
                SpawnOrbsFood();
        }

        
        
        private void SpawnLevel()
        {
            var camScale = new Vector3(Cam!.pixelWidth, Cam!.pixelHeight, 0);
            Instantiate(backgroundPrefab, Vector3.zero, quaternion.identity);
            backgroundPrefab.transform.localScale = camScale * FieldSizeMultiplier;
        }

        
        
        private void SpawnPlayer()
        {
            var randomPosition = RandomPosition();
            var spawnPoint = randomPosition;
            Instantiate(playerPrefab, spawnPoint, quaternion.identity);
            Player = GameObject.FindWithTag("Player");

            var nameTag = Player.GetComponentInChildren<TMP_Text>();
            nameTag.text = $"{playerName}";
            
            Debug.Log($"{Player.gameObject.name} spawned at: {spawnPoint}");
            Cam.transform.position = Player.transform.position;
        }

        
        
        private void CalculateSpawnRange()
        {
            var fieldTrans = spawnZone.GetComponent<Transform>();
            fieldCenter = new Vector3(fieldTrans.position.x, fieldTrans.position.y, 0);

            fieldSize.x = fieldTrans.localScale.x * spawnZone.size.x;
            fieldSize.y = fieldTrans.localScale.y * spawnZone.size.y;
            
            Debug.Log("field size x: " + fieldSize.x);
            Debug.Log("field size y: " + fieldSize.y);
        }

        
        
        private void SpawnOrbsFood()
        {
            Instantiate(foodPrefab, RandomPosition(), Quaternion.identity, FoodHolder.transform);
        }

        
        
        private Vector3 RandomPosition()
        {
            Vector2 randomSpawn = new Vector3(Random.Range(-fieldSize.x / 2, fieldSize.x / 2),
                Random.Range(-fieldSize.y / 2, fieldSize.y / 2), 0);
            return randomSpawn + fieldCenter;
        }
        
        
        
        private IEnumerator RespawnUponDeath() // Respawn logic
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
