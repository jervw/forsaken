using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Jervw.Crimson
{
    public class EnemySpawn : MonoBehaviourPun
    {
        float spawnRate;

        void Start()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            spawnRate = LevelHandler.Instance.current.enemySpawnRate;
            Invoke("Interval", LevelHandler.Instance.current.enemySpawnDelay);
        }

        void Interval()
        {
            spawnRate -= LevelHandler.Instance.current.enemyIncreaseRate;

            if (LevelHandler.Instance.enemyCount <= LevelHandler.Instance.current.enemyCountMax)
            {
                SpawnEnemy();
                Invoke("Interval", spawnRate);
            }
        }

        void SpawnEnemy()
        {
            PhotonNetwork.Instantiate("Zombie", LevelHandler.Instance.GetRandomPos(), Quaternion.identity);
            LevelHandler.Instance.enemyCount++;
        }
    }
}
