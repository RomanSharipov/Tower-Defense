using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    [RequireComponent(typeof(EnemyBase))]
    public class EnemyEffects : MonoBehaviour 
    {
        private EnemyBase _enemy;

        private void Awake()
        {
            _enemy = GetComponent<EnemyBase>();
            _enemy.Died += PlayEffects;
        }

        private void OnDestroy()
        {
            _enemy.Died -= PlayEffects;
        }

        private void PlayEffects(EnemyBase enemy)
        {
            
        }
    }
}
