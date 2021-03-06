using System;
using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
        public float ScaleTolerance = 0.01f;
        private string PlayerName;
        public float foodSize;
        private void Start()
        {
            PlayerName = GetComponentInChildren<TMP_Text>().text;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var food = other.gameObject.CompareTag("Food");
            var enemy = other.gameObject.CompareTag("Player");
            var playerScale = gameObject.transform.localScale.x;

            if (food)
            {
                IncreasePlayerScale(playerScale, foodSize);
                IncreasePlayerScore(other);
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


        
        private void IncreasePlayerScale(float playerScale, float enemyScale) //Update Visuals
        {
            gameObject.transform.localScale += new Vector3(enemyScale, enemyScale, 0);
            var newScale = transform.localScale.x;


            Debug.Log($"Player scale increased by {newScale - playerScale}");
        }



        private void IncreasePlayerScore(Collision2D other)
        {
            var enemyScore = other.gameObject.GetComponent<Consume>() ? // null check to see if enemy via getting consume component
                other.gameObject.GetComponent<PlayerScore>().Score // increases score using rival's score value if they have PlayerScore component
                : 1; // otherwise increases score by 1 by default if not an enemy i.e. a food orb
            
            
            GetComponent<PlayerScore>().IncreaseScore(enemyScore);
        }



        private void CalculateEnemyOutcome(Collision2D other, float playerScale)
        {
            var enemyScale = other.gameObject.transform.localScale.x;
            var enemyName = other.gameObject.GetComponentInChildren<TMP_Text>().text;

            // Player wins, eats Enemy
            if (playerScale > enemyScale)
            {
                Debug.Log($"{gameObject.name} ate {enemyName}");
                
                IncreasePlayerScale(playerScale, enemyScale);
                IncreasePlayerScore(other);Destroy(other.gameObject);
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
