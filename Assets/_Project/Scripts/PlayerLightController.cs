using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NotEnough.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerLightController : MonoBehaviour
    {
        [Header("Light References")]
        [Tooltip("Arrastra aquí el componente Light2D físico de la esfera")]
        [SerializeField] private Light2D _playerLight;

        [Header("Reveal Ability Settings")]
        [SerializeField] private float _revealRadius = 10f;
        [SerializeField] private float _revealIntensity = 80f;
        [SerializeField] private Color _revealColor = Color.magenta;
        [Tooltip("Vida que se drena por segundo mientras se usa la habilidad.")]
        [SerializeField] private float _revealHealthDrainPerSecond = 10f;
        
        [Header("Camera Zoom Settings")]
        [Tooltip("Tamaño de la cámara ortográfica al usar la habilidad.")]
        [SerializeField] private float _revealCameraOrthoSize = 10f;
        [SerializeField] private float _cameraZoomSpeed = 5f;

        /// <summary>Otros scripts pueden leer esto para saber si el jugador está en modo Reveal.</summary>
        public bool IsRevealing { get; private set; }

        private PlayerHealth _playerHealth;
        private PlayerInput _playerInput;
        
        private Color _defaultColor;
        private float _defaultRadius;
        private float _defaultIntensity;
        
        private Camera _mainCamera;
        private float _defaultCameraOrthoSize;
        private float _savedTimeScale = 1f;
        private bool _wasRevealing;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            _playerInput = GetComponent<PlayerInput>();
            
            if (_playerLight != null)
            {
                _defaultColor = _playerLight.color;
                _defaultRadius = _playerLight.pointLightOuterRadius;
                _defaultIntensity = _playerLight.intensity;
            }
            
            _mainCamera = Camera.main;
            if (_mainCamera != null && _mainCamera.orthographic)
            {
                _defaultCameraOrthoSize = _mainCamera.orthographicSize;
            }
        }

        private void Update()
        {
            if (_playerLight == null) return;

            // Usamos unscaledDeltaTime para que las animaciones funcionen con Time.timeScale = 0
            float dt = Time.unscaledDeltaTime;

            if (_playerInput.RevealAbilityPressed)
            {
                // Al entrar en Reveal: pausamos el tiempo
                if (!_wasRevealing)
                {
                    _savedTimeScale = Time.timeScale;
                    Time.timeScale = 0f;
                    _wasRevealing = true;
                }

                IsRevealing = true;

                // Drenar vida en tiempo real (no afectado por la pausa)
                _playerHealth.ReduceHealth(_revealHealthDrainPerSecond * dt);

                // Animación de luz
                _playerLight.pointLightOuterRadius = Mathf.Lerp(_playerLight.pointLightOuterRadius, _revealRadius, dt * 15f);
                _playerLight.intensity = Mathf.Lerp(_playerLight.intensity, _revealIntensity, dt * 15f);
                _playerLight.color = Color.Lerp(_playerLight.color, _revealColor, dt * 15f);
                
                // Animación de cámara
                if (_mainCamera != null && _mainCamera.orthographic)
                {
                    _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _revealCameraOrthoSize, dt * _cameraZoomSpeed);
                }
            }
            else
            {
                // Al salir de Reveal: restauramos el tiempo
                if (_wasRevealing)
                {
                    Time.timeScale = _savedTimeScale;
                    _wasRevealing = false;
                }

                IsRevealing = false;

                // Regresamos a los valores por defecto (ya no se encoge por vida)
                _playerLight.pointLightOuterRadius = Mathf.Lerp(_playerLight.pointLightOuterRadius, _defaultRadius, dt * 10f);
                _playerLight.intensity = Mathf.Lerp(_playerLight.intensity, _defaultIntensity, dt * 10f);
                _playerLight.color = Color.Lerp(_playerLight.color, _defaultColor, dt * 10f);
                
                if (_mainCamera != null && _mainCamera.orthographic)
                {
                    _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _defaultCameraOrthoSize, dt * _cameraZoomSpeed);
                }
            }
        }
    }
}
