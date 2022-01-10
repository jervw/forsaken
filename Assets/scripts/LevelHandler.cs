using UnityEngine;
using Photon.Pun;

public class LevelHandler : MonoBehaviourPun
{
    public static LevelHandler Instance { get; private set; }

    [HideInInspector] public LevelData current;
    [HideInInspector] public int enemyCount = 0, enemyDeathCount = 0;
    [SerializeField] private LevelData levelData;

    SpriteRenderer ground;
    EdgeCollider2D edgeCol;

    void Awake()
    {
        Instance = this;
        current = levelData;

        ground = GetComponentInChildren<SpriteRenderer>();
        edgeCol = GetComponentInChildren<EdgeCollider2D>();
    }

    void Start() => SetBounds();

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        if (GetProgress() >= 1f)
            GameManager.Instance.ChangeState(GameManager.GameState.Win);
    }

    void SetBounds()
    {
        var minBounds = ground.GetComponentInChildren<SpriteRenderer>().bounds.min;
        var maxBounds = ground.GetComponentInChildren<SpriteRenderer>().bounds.max;

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

    public Vector2 GetRandomPos()
    {
        return new Vector2(
            Random.Range(current.minBounds.x, current.maxBounds.x),
            Random.Range(current.minBounds.y, current.maxBounds.y)
        );
    }

    public float GetProgress() => (float)enemyDeathCount / current.enemyCountMax;

    public void EnemyDeath() => enemyDeathCount++;
}
