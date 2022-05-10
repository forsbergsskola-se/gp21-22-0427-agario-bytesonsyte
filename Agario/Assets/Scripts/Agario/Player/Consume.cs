using TMPro;
using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            var food = other.gameObject.CompareTag("Food");
            var enemy = other.gameObject.CompareTag("Player");
            
            if (food)
            {
                Debug.Log($"{gameObject.name} ate {other.gameObject.name}");
                Destroy(other.gameObject);
            }
        }
    }
}
