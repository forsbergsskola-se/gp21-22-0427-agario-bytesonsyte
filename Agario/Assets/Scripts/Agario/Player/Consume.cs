using System;
using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
        public float ScaleTolerance = 0.01f;
        private string PlayerName;
        private int Score = 0;
        private TMP_Text ScoreUI;
        private void Start()
        {
            PlayerName = GetComponentInChildren<TMP_Text>().text;
            ScoreUI = FindObjectOfType<TextMeshProUGUI>().GetComponent<TMP_Text>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var food = other.gameObject.CompareTag("Food");
            var enemy = other.gameObject.CompareTag("Player");
            var playerScale = gameObject.transform.localScale.x;

            if (food)
            {
                var foodSize = other.gameObject.transform.localScale.x;
                IncreasePlayerScale(other, playerScale, foodSize);
                IncreasePlayerScore(other, true);
                ConsumeFood(other);
                return;
            }
            if (enemy)
                CalculateEnemyOutcome(other, playerScale);
        }

        
        
        private void ConsumeFood(Collision2D other)
        {
            Debug.Log($"{PlayerName} ate {other.gameObject.name}");
            Destroy(other.gameObject);
        }


        private void IncreasePlayerScale(Collision2D other, float playerScale, float enemyScale)
        {
            var newScale = gameObject.transform.localScale;
            newScale += new Vector3(enemyScale, enemyScale, 0);

            Debug.Log($"Player scale increased by {newScale.x - playerScale}");
        }



        private void IncreasePlayerScore(Collision2D other, bool food)
        {
            if (food)
                Score++;

            else
            {
                var enemyScore = other.gameObject.GetComponent<Consume>().Score;
                Score = +enemyScore;
            }

            ScoreUI.text = "Score: " + Score;
        }



        private void CalculateEnemyOutcome(Collision2D other, float playerScale)
        {
            var enemyScale = other.gameObject.transform.localScale.x;
            var enemyName = other.gameObject.GetComponentInChildren<TMP_Text>().text;

            // Player wins, eats Enemy
            if (playerScale > enemyScale)
            {
                Debug.Log($"{gameObject.name} ate {enemyName}");
                
                IncreasePlayerScale(other, playerScale, enemyScale);
                IncreasePlayerScore(other, false);
                Destroy(other.gameObject);
            }
            
            // It's a draw, both go free
            else if (Math.Abs(playerScale - enemyScale) < ScaleTolerance) // has a tolerance check to make up for inaccurate float comparisons
                Debug.Log($"{PlayerName} can't eat {enemyName} [Scale sizes are equal]");

            //Player loses, print impending death
            else
                Debug.Log($"{PlayerName} is being consumed by {enemyName} [Scale size higher: {enemyScale}:{playerScale}]");
        }
    }
}
