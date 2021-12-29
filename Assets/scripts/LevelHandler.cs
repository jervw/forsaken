using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance { get; private set; }

    public LevelData[] levelData;
    public LevelData current;
    [HideInInspector]
    public int enemyCount, enemyDeathCount;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SetLevelData(int levelIndex)
    {
        current = levelData[levelIndex];
        enemyCount = 0;
        enemyDeathCount = 0;
    }

    public void SetBounds(Vector2 minBounds, Vector2 maxBounds)
    {
        current.minBounds = minBounds;
        current.maxBounds = maxBounds;
    }

    public float GetProgress()
    {
        return (float)enemyDeathCount / current.enemyCountMax;
    }

    public Vector2 GetRandomPos()
    {
        return new Vector2(
            Random.Range(current.minBounds.x, current.maxBounds.x),
            Random.Range(current.minBounds.y, current.maxBounds.y)
        );
    }
}
