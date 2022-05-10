using System;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

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
                Debug.Log($"{PlayerName} ate {other.gameObject.name}");
                Destroy(other.gameObject);
                return;
            }

            if (!enemy) return;
            
            var playerScale = transform.localScale.x;
            var enemyScale = other.gameObject.transform.localScale.x;
            var enemyName = other.gameObject.GetComponentInChildren<TMP_Text>().text;

            if (playerScale > enemyScale)
            {
                Debug.Log($"{gameObject.name} ate {enemyName}");
                //TODO: also consume enemy score
                Destroy(other.gameObject);
            }
                
            else if (Math.Abs(playerScale - enemyScale) < ScaleTolerance)
                Debug.Log($"{PlayerName} can't eat {enemyName} [Scale sizes are equal]");

            else
                Debug.Log($"{PlayerName} is being consumed by {enemyName} [Scale size higher: {enemyScale}:{playerScale}]");
        }
    }
}
