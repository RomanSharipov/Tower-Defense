using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameDamageTrigger : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _damageTrigger;
        [SerializeField] private float _maxHeight;
        [SerializeField] private float _speedGrain;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private Transform _idlePosition;

        private float _height;
        public event Action<EnemyBase> EnemyEntered;

        private Dictionary<Collider, EnemyBase> _enemies = new Dictionary<Collider, EnemyBase>();
        
        private void Start()
        {
            _damageTrigger
                .OnTriggerStayAsObservable()
                .Sample(TimeSpan.FromSeconds(0.2f))
                .Subscribe(other => HandleTriggerStay(other))
                .AddTo(this); 
        }

        private void HandleTriggerStay(Collider other)
        {
            if (_enemies.TryGetValue(other, out EnemyBase enemy))
            {
                EnemyEntered?.Invoke(enemy);
            }

            if (other.TryGetComponent(out EnemyBase enemyBase))
            {
                EnemyEntered?.Invoke(enemyBase);
                _enemies[other] = enemyBase;
            }
        }

        public void OnStartAttack()
        {
            _damageTrigger.transform.localPosition = _attackPosition.localPosition;
            _damageTrigger.enabled = true;
        }

        public void ResetTrigger()
        {
            _damageTrigger.transform.localPosition = _idlePosition.localPosition;
            _height = 0.0f;
            _damageTrigger.height = 1.0f;
            _damageTrigger.center.Set(0, 0, 1);
            _damageTrigger.enabled = false;
        }

        public void UpdateScaleAndPosition()
        {
            _height += Time.deltaTime * _speedGrain;

            _height = Mathf.Min(_height, _maxHeight);

            _damageTrigger.height = Mathf.Abs(_height);
            
            _damageTrigger.center = new Vector3(0, 0, _height / 2);
        }
    }
}
