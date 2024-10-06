using UnityEngine;

public static class TimeManager
{
    private static float elapsedTime;

    public static void UpdateTime()
    {
        elapsedTime += Time.deltaTime;
    }

    // Method to get formatted game time in hours/minutes/seconds
    public static string GetFormattedGameTime()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Method to get the elapsed time
    public static float GetElapsedTime()
    {
        return elapsedTime;
    }
}
