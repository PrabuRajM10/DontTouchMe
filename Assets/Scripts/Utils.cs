public static class Utils
{

    public static float GetValueByPercentage(float min , float max , float percentage)
    {
        return min + (percentage / 100) * (max - min);
    }
}