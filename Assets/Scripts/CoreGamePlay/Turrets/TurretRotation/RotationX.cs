using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationX : MonoBehaviour
    {
        public void RotateTowards(Vector3 targetDirection, float rotationSpeed)
        {

            
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection); 
            float angleX = lookRotation.eulerAngles.x;









            
            transform.localRotation = Quaternion.Euler(angleX, 0, 0);
        }
    }
}
