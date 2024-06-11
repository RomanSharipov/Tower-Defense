using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SetterMaterial", menuName = "SetterMaterial")]
public class SetterMaterial : ScriptableObject
{
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private GameObject[] _prefabs;

    [ContextMenu("AssignMaterialToPrefabs()")]
    public void AssignMaterialToPrefabs()
    {
        foreach (GameObject prefab in _prefabs)
        {
            if (prefab != null)
            {
                MeshRenderer[] meshRenderers = prefab.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer meshRenderer in meshRenderers)
                {
                    if (meshRenderer != null)
                    {
                        meshRenderer.sharedMaterial = _targetMaterial;
                        EditorUtility.SetDirty(meshRenderer.gameObject);
                    }
                }
            }
        }

        // Mark the ScriptableObject as dirty to save changes
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}
