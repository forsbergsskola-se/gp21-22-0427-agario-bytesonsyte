using UnityEngine;

namespace Agario.Player
{
    public class Consume : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Food"))
                Debug.Log($"{gameObject.name} ate {other.gameObject.name}");

        }
    }
}
