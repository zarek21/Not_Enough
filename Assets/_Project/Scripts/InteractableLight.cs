using UnityEngine;

namespace NotEnough.World
{
    public abstract class InteractableLight : MonoBehaviour
    {
        // Protected significa que los hijos pueden verla pero otros scripts no.
        [SerializeField] protected float amount = 20f;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnInteract(collision.gameObject);
                Destroy(gameObject); 
            }
        }
        protected abstract void OnInteract(GameObject player);
    }
}
