using UnityEngine;

namespace Com.Jervw.Crimson
{
    public class EnemySpawn : MonoBehaviour
    {
        Vector2 min, max;
        float spawnRate, spawnBorder = 2f;

        void Start()
        {
            spawnRate = LevelHandler.Instance.current.enemySpawnRate;
            min = LevelHandler.Instance.current.minBounds + new Vector2(spawnBorder, spawnBorder);
            max = LevelHandler.Instance.current.maxBounds - new Vector2(spawnBorder, spawnBorder);

            Invoke("Interval", LevelHandler.Instance.current.enemySpawnDelay);
        }

        void Interval()
        {
            spawnRate -= LevelHandler.Instance.current.enemyIncreaseRate;

            if (LevelHandler.Instance.enemyCount < LevelHandler.Instance.current.enemyCountMax)
            {
                SpawnEnemy();
                Invoke("Interval", spawnRate);
            }
        }

        void SpawnEnemy()
        {
            var spawnPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            LevelHandler.Instance.enemyCount++;
        }
    }
}
