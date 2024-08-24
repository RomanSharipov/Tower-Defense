using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTurret : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _red;
    [SerializeField] private Material _transparent;

    public void SetTransparent()
    {
        foreach (MeshRenderer renderer in _renderers) 
        {
            renderer.material = _transparent;
        }
    }
    public void SetRed()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material = _red;
        }
    }
}
