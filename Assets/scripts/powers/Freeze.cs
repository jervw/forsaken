using UnityEngine;
using Photon.Pun;

public class Freeze : MonoBehaviourPun
{
    [SerializeField]
    float freezeTime;

    void Start()
    {
        if (!photonView.IsMine)
            Destroy(this);
        else
            photonView.RPC("FreezeEnemies", RpcTarget.All);
    }

    [PunRPC]
    void FreezeEnemies()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
            StartCoroutine(enemy.GetComponent<Enemy>().FreezeEnemy(freezeTime));
    }
}
