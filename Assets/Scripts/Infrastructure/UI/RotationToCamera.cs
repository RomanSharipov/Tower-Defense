using UnityEngine;

public class RotationToCamera : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _enabled;

    public void Init(Camera camera)
    {
        _mainCamera = camera;
        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled)
            return;

        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
}
