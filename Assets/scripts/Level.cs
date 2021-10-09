using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject ground;

    private Vector2 minBounds, maxBounds;

    void Start()
    {
        EdgeCollider2D edgeCol = GetComponentInChildren<EdgeCollider2D>();

        var groundSprite = ground.GetComponent<SpriteRenderer>();

        minBounds = groundSprite.bounds.min;
        maxBounds = groundSprite.bounds.max;

        var botLeftCorner = minBounds;
        var topLeftCorner = new Vector2(minBounds.x, -minBounds.y);
        var topRightCorner = maxBounds;
        var botRightCorner = new Vector2(maxBounds.x, -maxBounds.y);


        Vector2[] points = edgeCol.points;
        points.SetValue(botLeftCorner, 0);
        points.SetValue(topLeftCorner, 1);
        points.SetValue(topRightCorner, 2);
        points.SetValue(botRightCorner, 3);
        points.SetValue(botLeftCorner, 4);
        edgeCol.points = points;

    }



}
