using UnityEngine;

namespace NotEnough.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private float _jumpForce = 12f;
        [Tooltip("Fuerza del salto impulsado por la luz de salto.")]
        [SerializeField] private float _impulseJumpForce = 18f;
        
        [Header("Impulse Jump Settings")]
        [Tooltip("Costo de vida por usar el doble salto de luz en el aire.")]
        [SerializeField] private float _impulseJumpHealthCost = 15f;

        [Header("Ground Detection")]
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Gravity")]
        [SerializeField] private float _gravityScale = 10f;

        public bool CanUseImpulseJump { get; set; }

        private Rigidbody2D _rb2D;
        private PlayerInput _playerInput;
        private PlayerHealth _playerHealth;
        private PlayerLightController _lightController;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _playerHealth = GetComponent<PlayerHealth>();
            _lightController = GetComponent<PlayerLightController>();
        }

        private void Update()
        {
            if (_lightController != null && _lightController.IsRevealing)
            {
                _rb2D.linearVelocity = Vector2.zero;
                _rb2D.gravityScale = 0f;
                return;
            }
            
            _rb2D.gravityScale = _gravityScale;

            HandleJump();
        }

        private void FixedUpdate()
        {
            if (_lightController != null && _lightController.IsRevealing) return;
            HandleMovement();
        }

        private void HandleMovement()
        {
            _rb2D.linearVelocity = new Vector2(_playerInput.HorizontalInput * _moveSpeed, _rb2D.linearVelocity.y);
        }

        private void HandleJump()
        {
            if (_playerInput.JumpPressed && CheckIfGrounded())
            {
                PerformJump(_jumpForce);
            }
            else if (_playerInput.JumpPressed && CanUseImpulseJump)
            {
                PerformJump(_impulseJumpForce);
                _playerHealth.ReduceHealth(_impulseJumpHealthCost); 
                CanUseImpulseJump = false; 
            }
        }

        private void PerformJump(float force)
        {
            _rb2D.linearVelocity = new Vector2(_rb2D.linearVelocity.x, 0f);
            _rb2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            _playerInput.ConsumeJumpInput();
        }

        private bool CheckIfGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        }

        private void OnDrawGizmosSelected()
        {
            if (_groundCheckPoint != null)
            {
                // Cambia de color para ayudarnos a debugear la colisión en tiempo real
                Gizmos.color = CheckIfGrounded() ? Color.green : Color.red;
                Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
            }
        }
    }
}
