namespace Assets.Scripts.CoreGamePlay
{
    public interface IAttackComponent : IUpgradeable
    {
        public void AttackIfNeeded(EnemyBase enemyBase);
        public void OnStartAttack();
        public void OnEndAttack();
    }
}
