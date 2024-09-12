using UnityEngine;

namespace CodeBase.Helpers
{
    public static class HexCalculator
    {
        public static Vector3 ToWorldPosition(int q, int r, float tileSpacing)
        {
            float x = tileSpacing * Mathf.Sqrt(3) * (q + r / 2.0f);
            float z = tileSpacing * 1.5f * r;
            Vector3 position = new Vector3(x, 0, z);
            return position;
        }
    }
}
