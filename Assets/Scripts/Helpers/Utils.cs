using UnityEngine;

namespace Helpers
{
    public static class Utils
    {

        public static float GetValueByPercentage(float min , float max , float percentage)
        {
            return min + (percentage / 100) * (max - min);
        }
        
        
        public static Vector2 GetRandomPointAroundTarget(Vector3 targetPos , float scale)
        {
            return Random.insideUnitCircle * scale + new Vector2(targetPos.x, targetPos.z);
        }
    }
}