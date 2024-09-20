using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationY : MonoBehaviour
    {
        public void RotateTowards(Vector3 targetDirection, float rotationSpeed)
        {
            Quaternion lookRot = Quaternion.LookRotation(targetDirection);
            lookRot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
        }
    }
}
