using Assets.Scripts.CoreGamePlay;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CodeBase.Infrastructure.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private Color _minValue;
        [SerializeField] private Color _maxValue;

        private IEnemyHealth _health;

        private void Awake()
        {
            _health = GetComponent<IEnemyHealth>();

            _health.CurrentHealth.Subscribe(value =>
            {
                _slider.value = (float)value / _health.MaxHealth;
            }).AddTo(this);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _fill.color = Color.Lerp(_minValue, _maxValue, value);
        }
    }
}