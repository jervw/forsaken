using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelData : ScriptableObject
{
    public string sceneName;
    public string levelName;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;
    [Header("Pickup settings")]
    public float pickupChance;
    [Header("Enemy settings")]
    public int enemyCountMax;
    public float enemySpawnDelay, enemySpawnRate, enemyIncreaseRate;

    [Header("Music")]
    public Sound music;

    [HideInInspector]
    public Vector2 minBounds, maxBounds;
}
