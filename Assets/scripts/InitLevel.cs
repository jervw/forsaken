using UnityEngine;
using Photon.Pun;

public class InitLevel : MonoBehaviourPunCallbacks
{
    public GameObject ground;

    EdgeCollider2D edgeCol;
    SpriteRenderer groundSprite;

    Vector2 minBounds, maxBounds;
    bool initialized = false;

    void Awake()
    {
        edgeCol = GetComponentInChildren<EdgeCollider2D>();
        groundSprite = ground.GetComponent<SpriteRenderer>();
        Init();
    }

    void Init()
    {
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

        LevelData.SetBounds(minBounds, maxBounds);

        PhotonNetwork.InstantiateRoomObject("EnemySpawner", Vector3.zero, Quaternion.identity);
    }
}
