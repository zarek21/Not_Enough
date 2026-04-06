using UnityEngine;
using NotEnough.Player;

namespace NotEnough.World
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ImpulseJumpZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            var movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.CanUseImpulseJump = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            var movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.CanUseImpulseJump = false;
            }
        }
    }
}
