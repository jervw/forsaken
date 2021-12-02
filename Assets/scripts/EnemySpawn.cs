using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        Level levelDetails = GameObject.Find("Level").GetComponent<Level>();
        Vector2 size = levelDetails.GetSize();


        // Spawn enemies
        for (int i = 0; i < levelDetails.GetEnemyCount(); i++)
        {
            GameObject e = Instantiate(enemy, levelDetails.GetRandomPosition(), Quaternion.identity) as GameObject;
        }

    }

}
