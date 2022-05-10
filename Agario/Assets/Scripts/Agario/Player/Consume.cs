using System;
using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
        public float ScaleTolerance = 0.01f;
        private string PlayerName;
        private void Start()
        {
            PlayerName = GetComponentInChildren<TMP_Text>().text;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var food = other.gameObject.CompareTag("Food");
            var enemy = other.gameObject.CompareTag("Player");

            if (food)
            {
                ConsumeFood(other);
                //TODO: increase score
                //TODO: inscrease size
                return;
            }
            
            if (!enemy) return;
            CalculateEnemyOutcome(other);
        }

        
        
        private void ConsumeFood(Collision2D other)
        {
            Debug.Log($"{PlayerName} ate {other.gameObject.name}");
            Destroy(other.gameObject);
        }

        
        
        private void CalculateEnemyOutcome(Collision2D other)
        {
            var playerScale = transform.localScale.x;
            var enemyScale = other.gameObject.transform.localScale.x;
            var enemyName = other.gameObject.GetComponentInChildren<TMP_Text>().text;

            // Player wins, eats Enemy
            if (playerScale > enemyScale)
            {
                Debug.Log($"{gameObject.name} ate {enemyName}");
                Destroy(other.gameObject);
                //TODO: also consume enemy score
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
