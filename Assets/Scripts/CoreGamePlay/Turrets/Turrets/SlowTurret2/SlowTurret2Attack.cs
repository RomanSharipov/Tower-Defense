using UniRx;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class SlowTurret2Attack : AttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;
        [SerializeField] private FlameDamageTrigger _flameDamageTrigger;

        private ParticleSystemCollection _currentEffects;
        private bool _nowAttack;
        
        public override void OnStartAttack(EnemyBase enemyBase)
        {
            _nowAttack = true;
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
            _flameDamageTrigger.OnStartAttack();
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
                        _flameDamageTrigger.ResetTrigger();
                    }
                })
                .AddTo(this);
        }

        public override void Attack(EnemyBase enemyBase)
        {
            _flameDamageTrigger.UpdateScaleAndPosition();
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
        }

        private void OnEnable()
        {
            _flameDamageTrigger.EnemyEntered += OnEnemyEntered;
        }

        private void OnDisable()
        {
            _flameDamageTrigger.EnemyEntered -= OnEnemyEntered;
        }

        private void OnEnemyEntered(EnemyBase enemy)
        {
            enemy.TakeDamage(_damage);
            enemy.EnemyMovement.SlowDownMovement(70, 3.0f);
        }
    }
}
