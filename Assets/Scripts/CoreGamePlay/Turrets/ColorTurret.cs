using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTurret : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _red;
    [SerializeField] private Material _transparent;
    [SerializeField] private Material _default;
    
    public void SetColor(ColorType color)
    {
        switch (color)
        {
            case ColorType.None:
                break;
            case ColorType.BlockBuildColor:
                SetMaterial(_red);
                break;
            case ColorType.TransparentColor:
                SetMaterial(_transparent);
                break;
            case ColorType.DefaultColor:
                SetMaterial(_default);
                break;
        }
    }

    private void SetMaterial(Material material)
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material = material;
        }
    }
}

public enum ColorType
{
    None,
    BlockBuildColor,
    TransparentColor,
    DefaultColor,
}
