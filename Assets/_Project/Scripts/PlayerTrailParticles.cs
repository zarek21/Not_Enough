using UnityEngine;

namespace NotEnough.Player
{
    public class PlayerTrailParticles : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("El Rigidbody del jugador para leer la velocidad.")]
        [SerializeField] private Rigidbody2D _playerRb;
        [Tooltip("El sistema de partículas hijo.")]
        [SerializeField] private ParticleSystem _trailParticles;

        [Header("Settings")]
        [Tooltip("Distancia desde el centro (en X) donde aparecen las partículas.")]
        [SerializeField] private float _offsetDistance = 0.5f;
        [Tooltip("Velocidad mínima para activar las partículas.")]
        [SerializeField] private float _minVelocityToEmit = 0.1f;

        private ParticleSystem.EmissionModule _emissionModule;

        private void Awake()
        {
            if (_trailParticles != null)
            {
                _emissionModule = _trailParticles.emission;
            }
        }

        private void Update()
        {
            if (_playerRb == null || _trailParticles == null) return;

            float velocityX = _playerRb.linearVelocity.x;

            // Si el jugador está quieto, apagamos la emisión
            if (Mathf.Abs(velocityX) < _minVelocityToEmit)
            {
                _emissionModule.enabled = false;
                return;
            }

            _emissionModule.enabled = true;

            // Calculamos la dirección hacia donde DEBEN salir las partículas (opuesto al movimiento)
            float directionX = velocityX > 0 ? -1f : 1f;
            
            // Calculamos la posición en el mundo (no local) para ignorar si la bola está girando
            Vector3 worldOffset = new Vector3(directionX * _offsetDistance, 0f, 0f);
            _trailParticles.transform.position = _playerRb.transform.position + worldOffset;

            // Forzamos la rotación para que siempre miren hacia la izquierda o derecha en el mundo
            float targetRotationY = velocityX > 0 ? -90f : 90f;
            _trailParticles.transform.rotation = Quaternion.Euler(0f, targetRotationY, 0f);
        }
    }
}
