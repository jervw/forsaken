using UnityEngine;
using Com.Jervw.Crimson;

public class Freeze : MonoBehaviour
{
    [SerializeField]
    float freezeTime;

    void Start()
    {
        //if (!photonView.IsMine)
        //Destroy(this);
        //else photonView.RPC("FreezeEnemies", RpcTarget.All);
    }

    void FreezeEnemies()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
            StartCoroutine(enemy.GetComponent<Enemy>().FreezeEnemy(freezeTime));
    }
}
