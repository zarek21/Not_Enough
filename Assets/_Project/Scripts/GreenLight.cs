using UnityEngine;
using NotEnough.Player;

namespace NotEnough.World
{
    public class GreenLight : MonoBehaviour
    {
        [Header("Heal Settings")]
        [SerializeField] private float _healAmount = 20f;

        public void Collect(GameObject player)
        {
            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(_healAmount);
            }
            Destroy(gameObject);
        }
    }
}
