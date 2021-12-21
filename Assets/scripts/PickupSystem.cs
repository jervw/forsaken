using UnityEngine;
using Photon.Pun;

public class PickupSystem : MonoBehaviourPunCallbacks
{
    public PickupScriptable[] pickups;

    void Start()
    {
        InvokeRepeating("StateCheck", 10, 10);
    }

    void StateCheck()
    {
        if (LevelData.GetProgress() < 1)
            photonView.RPC("SpawnPickup", RpcTarget.All);
    }

    [PunRPC]
    void SpawnPickup()
    {
        int r = Random.Range(0, pickups.Length);
        var pickup = PhotonNetwork.Instantiate("Pickup", LevelData.GetRandomPosition(), Quaternion.identity) as GameObject;
        pickup.GetComponent<Pickup>().LoadData(pickups[r]);
        Debug.Log("Spawned pickup: " + pickups[r].name);
    }
}
