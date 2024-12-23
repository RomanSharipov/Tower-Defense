using Assets.Scripts.CoreGamePlay;
using UnityEngine;
using UnityEngine.UI;
using NTC.Pool;

namespace CodeBase.Infrastructure.UI
{
    public class HealthBar : MonoBehaviour 
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private Color _minValue;
        [SerializeField] private Color _maxValue;

        private IHealth _health;

        public void Init(IHealth health)
        {
            _health = health;
            _health.HealthChanged += OnHealthChanged;
            OnHealthChanged(_health.MaxHealth);
        }

        private void OnHealthChanged(int value)
        {
            _slider.value = (float)value / _health.MaxHealth;
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _fill.color = Color.Lerp(_minValue, _maxValue, value);
        }

        public void OnDespawn()
        {
            _health.HealthChanged -= OnHealthChanged;
        }
    }
}