using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelData
{
    public static Vector2 minBounds, maxBounds;
    public static int enemyCountMax = 20;
    public static int enemyCount = 0;
    public static int enemyDeathCount = 0;
    public static float enemySpawnDelay = 3f;
    public static float enemySpawnRate = 3f;
    public static float enemyIncreaseRate = 0.1f;

    public static void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }

    public static Vector2 GetRandomPosition()
    {
        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);
        return new Vector2(x, y);
    }

    public static float GetProgress()
    {
        return (float)enemyDeathCount / enemyCountMax;
    }

}