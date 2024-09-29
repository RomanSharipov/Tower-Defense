using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    [Serializable]
    public class BulletSpawnPoints 
    {
        [SerializeField] private Transform[] _spawnPoints;

        public Transform GetSpawnPointByIndex(int index)
        {
            return _spawnPoints[index];
        }
    }
}
