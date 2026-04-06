using UnityEngine;

namespace NotEnough.Player
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Verificamos si es el jugador
            if (other.TryGetComponent<PlayerHealth>(out var health))
            {
                health.Respawn();
            }
        }
    }
}
