using UnityEngine;
using NotEnough.Player;

namespace NotEnough.World
{
    [RequireComponent(typeof(Collider2D))]
    public class LightRechargeZone : MonoBehaviour
    {
        [Header("Recharge Settings")]
        [Tooltip("Cantidad de luz restaurada por segundo mientras el jugador está en la zona.")]
        [SerializeField] private float _healPerSecond = 25f;

        private PlayerHealth _playerInZone;

        private void Update()
        {
            if (_playerInZone != null)
            {
                _playerInZone.Heal(_healPerSecond * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInZone = collision.GetComponent<PlayerHealth>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInZone = null;
            }
        }
    }
}
