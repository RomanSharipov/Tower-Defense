using UnityEngine;
using VContainer;

public class RotationToCamera : MonoBehaviour
{
    private Camera _mainCamera;
    
    [Inject]
    public void Construct(Camera camera)
    {
        _mainCamera = camera;
    }

    private void LateUpdate()
    {
        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
}
