using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Jervw.Crimson
{
    public class EnemySpawn : MonoBehaviourPunCallbacks
    {
        public GameObject enemy;
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
            InvokeRepeating("Spawn", enemySpawnDelay, enemySpawnRate);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void Spawn()
        {
            if (enemiesSpawned <= levelDetails.GetEnemyCount())
            {
                Debug.Log("Spawning Enemy");
                PhotonNetwork.Instantiate(enemy.name, levelDetails.GetRandomPosition(), Quaternion.identity);
                enemiesSpawned++;
            }
            else
                CancelInvoke();
        }
    }
}
