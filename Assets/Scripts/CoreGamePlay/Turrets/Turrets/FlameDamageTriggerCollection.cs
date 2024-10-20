using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    [Serializable]
    public class FlameDamageTriggerCollection 
    {
        [SerializeField] private FlameDamageTrigger[] _flameDamageTriggers;

        public IReadOnlyCollection<FlameDamageTrigger> FlameDamageTriggers => _flameDamageTriggers;

        public FlameDamageTrigger GetParticleSystemByIndex(int index)
        {
            return _flameDamageTriggers[index];
        }

        public void ResetTrigger()
        {
            foreach (FlameDamageTrigger flameDamageTrigger in _flameDamageTriggers)
            {
                flameDamageTrigger.ResetTrigger();
            }
        }

        public void OnStartAttack()
        {
            foreach (FlameDamageTrigger flameDamageTrigger in _flameDamageTriggers)
            {
                flameDamageTrigger.OnStartAttack();
            }
        }

        public void UpdateScaleAndPosition()
        {
            foreach (FlameDamageTrigger flameDamageTrigger in _flameDamageTriggers)
            {
                flameDamageTrigger.UpdateScaleAndPosition();
            }
        }
    }
}
