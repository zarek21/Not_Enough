using System;
using UnityEngine;

namespace NotEnough.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        
        public event Action<float, float> OnHealthChanged;

        [Header("Settings")]
        [SerializeField] private float _maxHealth = 100f;
        [Tooltip("Cantidad de luz que el jugador pierde pasivamente por segundo.")]
        [SerializeField] private float _lightDrainPerSecond = 0.5f;

        [Header("Respawn")]
        [SerializeField] private Transform _spawnPoint;
        
        public float CurrentHealth { get; private set; }

        private void Start()
        {
            CurrentHealth = _maxHealth;
            OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        private void Update()
        {
            if (CurrentHealth > 0f)
            {
                ReduceHealth(_lightDrainPerSecond * Time.deltaTime);
            }
        }

        public void ReduceHealth(float amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, _maxHealth);
            
            OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);

            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, _maxHealth);
            
            OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);
        }

        private void Die()
        {
            Debug.Log("Game Over: Sin luz.");
            Respawn();
        }

        public void Respawn()
        {
            if (_spawnPoint != null)
            {
                // Teletransportar físicamente
                transform.position = _spawnPoint.position;
                
                // Resetear velocidad para que no siga cayendo
                if (TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }

                // Restaurar luz inicial
                CurrentHealth = _maxHealth;
                OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);
            }
            else
            {
                Debug.LogWarning("¡Falta asignar el SpawnPoint en el PlayerHealth!");
                // Fallback: Reiniciar escena entera
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }
}
