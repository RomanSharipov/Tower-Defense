namespace Assets.Scripts.CoreGamePlay
{
    public interface IAttackComponent : IUpgradeable
    {
        public void TryAttack(EnemyBase enemyBase);
    }
}
