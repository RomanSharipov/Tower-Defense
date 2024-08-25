using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTurret : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Material _red;
    [SerializeField] private Material _transparent;



    public void SetColor(Color color)
    {
        switch (color)
        {
            case Color.None:
                break;
            case Color.BlockBuildColor:
                SetMaterial(_red);
                break;
            case Color.DefaultColor:
                SetMaterial(_transparent);
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

public enum Color
{
    None,
    BlockBuildColor,
    DefaultColor,
}
