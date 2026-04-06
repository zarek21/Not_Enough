using UnityEngine;

namespace NotEnough.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public float HorizontalInput { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool RevealAbilityPressed { get; private set; }
        
        [Header("Settings")]
        [Tooltip("Buffer (en segundos) para el salto antes de tocar el suelo.")]
        [SerializeField] private float _jumpBufferTime = 0.15f;
        
        private float _jumpBufferCounter;

        private void Update()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            RevealAbilityPressed = Input.GetMouseButton(1);

            if (Input.GetButtonDown("Jump")) // Espacio por defecto
            {
                _jumpBufferCounter = _jumpBufferTime;
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            JumpPressed = _jumpBufferCounter > 0f;
        }

        public void ConsumeJumpInput()
        {
            _jumpBufferCounter = 0f;
            JumpPressed = false;
        }
    }
}
