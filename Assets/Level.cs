using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform ground;
    public SpriteRenderer sprite;
    public Camera cam;

    private Vector2 minBounds, maxBounds;

    void Start()
    {
        EdgeCollider2D mapCol = gameObject.GetComponentInChildren<EdgeCollider2D>();

        minBounds = sprite.bounds.min;
        maxBounds = sprite.bounds.max;

        var botLeftCorner = minBounds;
        var topLeftCorner = new Vector2(minBounds.x, -minBounds.y);
        var topRightCorner = maxBounds;
        var botRightCorner = new Vector2(maxBounds.x, -maxBounds.y);


        Vector2[] points = mapCol.points;
        points.SetValue(botLeftCorner, 0);
        points.SetValue(topLeftCorner, 1);
        points.SetValue(topRightCorner, 2);
        points.SetValue(botRightCorner, 3);

        mapCol.points = points;

    }



}
