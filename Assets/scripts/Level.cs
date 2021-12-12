using UnityEngine;

public class Level : MonoBehaviour
{
    public int enemyCount;
    public GameObject ground;


    Vector2 minBounds, maxBounds;
    bool initialized = false;

    void Start()
    {
        InitLevel();
    }

    void InitLevel()
    {
        if (initialized) return;

        var edgeCol = GetComponentInChildren<EdgeCollider2D>();
        var groundSprite = ground.GetComponent<SpriteRenderer>();

        minBounds = groundSprite.bounds.min;
        maxBounds = groundSprite.bounds.max;

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
        initialized = true;
    }


    public Vector2 GetSize() { return minBounds; }


    public Vector2 GetRandomPosition()
    {
        InitLevel();
        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);
        return new Vector2(x, y);
    }

}
