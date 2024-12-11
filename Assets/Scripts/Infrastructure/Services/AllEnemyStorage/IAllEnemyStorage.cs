using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IAllEnemyStorage
    {
        public int Count { get;}

        public void Add(EnemyBase enemyBase);
        public void Remove(EnemyBase enemyBase);
    }
}