using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;

        public void SetColor(Color color)
        {
            _colorTurret.SetColor(color);
        }
    }
}
