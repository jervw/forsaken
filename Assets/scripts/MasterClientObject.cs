using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class MasterClientObject : MonoBehaviourPunCallbacks
{
    void Start()
    {
        this.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        this.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
}
