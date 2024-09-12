using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Helpers
{
    public static class SceneHelper
    {
        public static bool TryGetRoot<T>(this Scene scene, out T result)
        {
            bool instanceFound = false;
            result = default;

            foreach (GameObject gameObject in scene.GetRootGameObjects())
            {
                if (gameObject.TryGetComponent(out T component))
                {
                    if (instanceFound)
                    {
                        throw new Exception($"Found multiple instances of type {typeof(T)}");
                    }
                    result = component;
                    instanceFound = true;
                }
            }
            if (instanceFound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
