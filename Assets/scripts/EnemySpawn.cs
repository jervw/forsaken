using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Jervw.Crimson
{
    public class EnemySpawn : MonoBehaviourPunCallbacks
    {
        Level levelDetails;

        int enemiesSpawned;

        float enemySpawnDelay = 3f;
        float enemySpawnRate = 3f;


        void Awake()
        {
            levelDetails = GameObject.Find("Level").GetComponent<Level>();
        }

        void Start()
        {
            Invoke("Interval", enemySpawnDelay);
        }

        void Interval()
        {
            enemySpawnRate -= .03f;

            if (enemiesSpawned <= levelDetails.enemyCount)
            {
                SpawnEnemy();
                Invoke("Interval", enemySpawnRate);
            }
        }

        void SpawnEnemy()
        {
            PhotonNetwork.Instantiate("Zombie", levelDetails.GetRandomPosition(), Quaternion.identity);
            enemiesSpawned++;
        }
    }
}
