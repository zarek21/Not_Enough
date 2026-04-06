using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NotEnough.World
{
    public class LightPulsator : MonoBehaviour
    {
        [Header("Pulse Settings")]
        [SerializeField] private float _pulseSpeed = 2f;
        [SerializeField] private float _minAlpha = 0f;
        [SerializeField] private float _maxAlpha = 1f;
        [SerializeField] private float _minIntensity = 10f;
        [SerializeField] private float _maxIntensity = 255f;

        [Header("Visual References")]
        [SerializeField] private SpriteRenderer _visualSprite;
        [SerializeField] private Light2D _light2D;

        private void Update()
        {
            float t = (Mathf.Sin(Time.time * _pulseSpeed) + 1f) / 2f;

            if (_visualSprite != null)
            {
                Color c = _visualSprite.color;
                c.a = Mathf.Lerp(_minAlpha, _maxAlpha, t);
                _visualSprite.color = c;
            }

            if (_light2D != null)
            {
                _light2D.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, t);
            }
        }
    }
}
