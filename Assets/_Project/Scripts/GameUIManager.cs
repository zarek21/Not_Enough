using UnityEngine;
using UnityEngine.UIElements;
using NotEnough.Player;

namespace NotEnough.Systems
{
    [RequireComponent(typeof(UIDocument))]
    public class GameUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerHealth _playerHealth;

        [Header("Score Settings")]
        [SerializeField] private int _totalPurpleOrbs = 10;

        private int _collectedPurpleOrbs = 0;

        private Label _healthText;
        private VisualElement _healthBar;
        private Label _scoreText;
        private VisualElement _victoryScreen;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _healthText = root.Q<Label>("health-text"); 
            _healthBar = root.Q<VisualElement>("health-bar"); 
            _scoreText = root.Q<Label>("score-text"); 
            _victoryScreen = root.Q<VisualElement>("victory-screen");
            
            UpdateScoreUI();
        }

        private void OnEnable()
        {
            if (_playerHealth != null)
                _playerHealth.OnHealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            if (_playerHealth != null)
                _playerHealth.OnHealthChanged -= HandleHealthChanged;
        }

        private void Start()
        {
            if (_playerHealth != null)
                HandleHealthChanged(_playerHealth.CurrentHealth, 100f);
        }

        private void HandleHealthChanged(float currentHealth, float maxHealth)
        {
            float healthRatio = Mathf.Clamp01(currentHealth / maxHealth);
            int percentage = Mathf.CeilToInt(healthRatio * 100f);

            if (_healthText != null)
                _healthText.text = $"{percentage}%";

            if (_healthBar != null)
                _healthBar.style.width = new Length(healthRatio * 100f, LengthUnit.Percent);
        }

        public void AddPurpleOrb()
        {
            _collectedPurpleOrbs++;
            UpdateScoreUI();

            if (_collectedPurpleOrbs >= _totalPurpleOrbs)
            {
                ShowVictoryScreen();
            }
        }

        private void ShowVictoryScreen()
        {
            if (_victoryScreen != null)
            {
                _victoryScreen.style.display = DisplayStyle.Flex;
                
                // Opcional: Ocultamos el resto de la UI para que se vea limpio como tu diseño
                _healthText.parent.style.display = DisplayStyle.None;
                _scoreText.parent.style.display = DisplayStyle.None;
            }
        }

        private void UpdateScoreUI()
        {
            if (_scoreText != null)
            {
                _scoreText.text = $"{_collectedPurpleOrbs}/{_totalPurpleOrbs}";
            }
        }
    }
}
