using UnityEngine;
using Photon.Pun;

public class LevelHandler : MonoBehaviourPun
{
    public static LevelHandler Instance { get; private set; }

    [HideInInspector] public LevelData current;
    [HideInInspector] public int enemyCount, enemyDeathCount;
    [SerializeField] private LevelData levelData;

    GameObject ground;
    EdgeCollider2D edgeCol;

    private void Awake()
    {
        Instance = this;
        current = levelData;
        enemyCount = 0;
        enemyDeathCount = 0;
    }

    public void SetBounds()
    {
        var minBounds = ground.GetComponent<SpriteRenderer>().bounds.min;
        var maxBounds = ground.GetComponent<SpriteRenderer>().bounds.max;

        Vector2 botLeftCorner = minBounds;
        Vector2 topLeftCorner = new Vector2(minBounds.x, -minBounds.y);
        Vector2 topRightCorner = maxBounds;
        Vector2 botRightCorner = new Vector2(maxBounds.x, -maxBounds.y);

        Vector2[] points = edgeCol.points;
        points.SetValue(botLeftCorner, 0);
        points.SetValue(topLeftCorner, 1);
        points.SetValue(topRightCorner, 2);
        points.SetValue(botRightCorner, 3);
        points.SetValue(botLeftCorner, 4);
        edgeCol.points = points;

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

    public void EnemyDeath()
    {
        enemyDeathCount++;
    }
}
