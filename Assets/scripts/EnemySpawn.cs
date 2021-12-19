using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Jervw.Crimson
{
    public class EnemySpawn : MonoBehaviourPunCallbacks
    {
        float spawnRate;

        void Start()
        {
            spawnRate = LevelData.enemySpawnRate;
            Invoke("Interval", LevelData.enemySpawnDelay);
        }

        void Interval()
        {
            spawnRate -= .03f;

            if (LevelData.enemyCount <= LevelData.enemyCountMax)
            {
                SpawnEnemy();

                Invoke("Interval", spawnRate);
            }
        }

        void SpawnEnemy()
        {

            PhotonNetwork.Instantiate("Zombie", LevelData.GetRandomPosition(), Quaternion.identity);
            LevelData.enemyCount++;
        }
    }
}
