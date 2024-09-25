namespace Assets.Scripts.CoreGamePlay
{
    public interface IAttackComponent : IUpgradeable
    {
        public void AttackIfNeeded();
        public void OnStartAttack(EnemyBase enemyBase);
        public void OnEndAttack();
    }
}
