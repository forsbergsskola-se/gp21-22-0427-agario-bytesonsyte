using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
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
            }
        }
    }
}
