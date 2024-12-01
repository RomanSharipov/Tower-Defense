using UnityEngine;

public class MinigunAnimation : MonoBehaviour
{
    [SerializeField] private SmoothRotateZ[] _barrels;

    public void StartAnimation()
    {
        foreach (var barrel in _barrels)
        {
            barrel.StartRotate();
        }
    }
    public void StopAnimation()
    {
        foreach (var barrel in _barrels)
        {
            barrel.StopRotate();
        }
    }
}
