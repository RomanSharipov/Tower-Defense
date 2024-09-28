namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurretAttack : AttackComponent
    {
        public override void OnStartAttack(EnemyBase enemyBase)
        {
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
        }

        public override void OnEndAttack()
        {
            base.OnEndAttack();
            _currentEffects.Stop();
        }

        public override void Attack(EnemyBase enemyBase)
        {
            enemyBase.TakeDamage(_damage);
        }
    }
}
