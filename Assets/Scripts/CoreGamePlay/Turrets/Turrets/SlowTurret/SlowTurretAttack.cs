using UniRx;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class SlowTurretAttack : AttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;
        [SerializeField] private FlameDamageTriggerCollection[] _flameDamageTriggerCollections;

        private ParticleSystemCollection _currentEffects;
        private FlameDamageTriggerCollection _currrentFlameDamageTrigger;
        private bool _nowAttack;
        
        public override void OnStartAttack(EnemyBase enemyBase)
        {
            _nowAttack = true;
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
            _currrentFlameDamageTrigger.OnStartAttack();
        }
        
        public override void OnEndAttack()
        {
            base.OnEndAttack();
            _currentEffects.Stop();
            _nowAttack = false;

            Observable.Timer(System.TimeSpan.FromSeconds(1))
                .Subscribe(_ => 
                {
                    if (!_nowAttack)
                    {
                        _currrentFlameDamageTrigger.ResetTrigger();
                    }
                })
                .AddTo(this);
        }

        public override void Attack(EnemyBase enemyBase)
        {
            _currrentFlameDamageTrigger.UpdateScaleAndPosition();
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _currrentFlameDamageTrigger = _flameDamageTriggerCollections[level];
        }

        private void OnEnemyEntered(EnemyBase enemy)
        {
            enemy.TakeDamage(_damage);
            enemy.EnemyMovement.SlowDownMovement(70, 3.0f);
        }

        private void OnEnable()
        {
            foreach (FlameDamageTriggerCollection collections in _flameDamageTriggerCollections)
            {
                foreach (FlameDamageTrigger triger in collections.FlameDamageTriggers)
                {
                    triger.EnemyEntered += OnEnemyEntered;
                }
            }
        }

        private void OnDisable()
        {
            foreach (FlameDamageTriggerCollection collections in _flameDamageTriggerCollections)
            {
                foreach (FlameDamageTrigger triger in collections.FlameDamageTriggers)
                {
                    triger.EnemyEntered -= OnEnemyEntered;
                }
            }
        }
    }
}
