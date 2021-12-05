using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


namespace Com.Jervw.Crimson
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        TMP_Text roomName;
        TMP_InputField roomNameInput;

        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        string gameVersion = "0.1";

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            roomName = GameObject.Find("RoomName").GetComponent<TMP_Text>();
            roomNameInput = GameObject.Find("InputField").GetComponent<TMP_InputField>();
            Connect();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OfflineMode()
        {
            Debug.Log("offline mode");
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.CreateRoom("Offline");
        }


        public void CreateLobby()
        {
            if (!PhotonNetwork.IsConnected) return;

            if (!PhotonNetwork.InRoom)
            {
                Debug.Log("CreateLobby()");
                string roomId = Random.Range(1000, 9999).ToString();
                PhotonNetwork.CreateRoom(roomId, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
                roomName.text = roomId;
                GameObject.Find("Main").SetActive(false);
            }
        }

        public void JoinLobby()
        {
            if (!PhotonNetwork.IsConnected) return;

            if (!PhotonNetwork.InRoom)
            {
                Debug.Log("JoinLobby()");
                PhotonNetwork.JoinRoom(roomNameInput.text);
            }
        }


        public void Connect()
        {
            Debug.Log("Connect()");
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnConnectedToMaster()
        {
            if (!PhotonNetwork.OfflineMode)
            {
                Debug.Log("Connected to master");
            }

        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning("Disconnected from master with reason: " + cause);
        }

        public override void OnJoinedRoom()
        {
            //Debug.Log("Joined room");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                //PhotonNetwork.LoadLevel(1);

            }

        }
    }
}
