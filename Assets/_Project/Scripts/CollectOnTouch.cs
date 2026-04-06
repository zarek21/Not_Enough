using UnityEngine;

namespace NotEnough.World
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CollectOnTouch : MonoBehaviour
    {
        private GreenLight _greenLight;
        private PurpleLight _purpleLight;

        private void Awake()
        {
            _greenLight = GetComponentInParent<GreenLight>();
            _purpleLight = GetComponentInParent<PurpleLight>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            if (_greenLight != null)
                _greenLight.Collect(collision.gameObject);

            if (_purpleLight != null)
                _purpleLight.Collect(collision.gameObject);
        }
    }
}
