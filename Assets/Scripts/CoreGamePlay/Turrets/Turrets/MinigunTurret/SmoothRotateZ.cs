using System.Collections;
using UnityEngine;

public class SmoothRotateZ : MonoBehaviour
{
    private float _maxSpeed = 1000f; 
    private float _accelerationTime = 0.5f; 
    private float _decelerationTime = 0.5f; 

    private float _currentSpeed = 0f;
    private Coroutine _rotationCoroutine;
    private bool _isRotating = false;

    private void Update()
    {
        if (_isRotating)
        {
            transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
        }
    }

    public void StartRotate()
    {
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
        }
        _rotationCoroutine = StartCoroutine(AdjustSpeed(_maxSpeed, _accelerationTime));
    }

    public void StopRotate()
    {
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
        }
        _rotationCoroutine = StartCoroutine(AdjustSpeed(0f, _decelerationTime));
    }

    private IEnumerator AdjustSpeed(float targetSpeed, float duration)
    {
        float startSpeed = _currentSpeed;
        float timeElapsed = 0f;

        _isRotating = true;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            _currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, timeElapsed / duration);
            yield return null;
        }

        _currentSpeed = targetSpeed;

        if (_currentSpeed == 0f)
        {
            _isRotating = false;
        }
    }
}
