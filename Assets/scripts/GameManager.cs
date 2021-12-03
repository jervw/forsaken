using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace Com.Jervw.Crimson
{

    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;

        static GameManager instance;

        void Awake()
        {
            /*
            DontDestroyOnLoad(this);
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
            */
        }

        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Lobby");
                return;
            }


            if (playerPrefab == null)
                Debug.LogError("Missing playerPrefab reference");
            else
                PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity, 0);
        }



        public void LeaveRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Leaving room and disconnecting from master");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
            else
                SceneManager.LoadScene(0);

        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadLevel()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Loading level");
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}
