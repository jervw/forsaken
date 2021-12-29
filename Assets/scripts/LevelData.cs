using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelData : ScriptableObject
{
    [Header("Pickup settings")]
    public float pickupChance;
    [Header("Enemy settings")]
    public int enemyCountMax;
    public float enemySpawnDelay, enemySpawnRate, enemyIncreaseRate;

    [HideInInspector]
    public Vector2 minBounds, maxBounds;
}
