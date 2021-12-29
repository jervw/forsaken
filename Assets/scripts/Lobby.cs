using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


namespace Com.Jervw.Crimson
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        public static Lobby Instance;
        public const byte MAX_PLAYERS = 4;

        public Button joinButton;
        public TMP_Text roomName, playerList;
        public TMP_InputField roomNameInput;

        MainMenuState mainState;

        void Awake()
        {
            mainState = GetComponent<MainMenuState>();
            Instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
            mainState.SetState(MainMenuState.MenuState.Main);

        }

        void UpdatePlayerList()
        {
            string players = "";
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                players += p.NickName;
                if (p.IsMasterClient)
                    players += " (host)";
                players += "\n";
            }
            playerList.text = players;
        }

        public void OfflineMode()
        {
            PhotonNetwork.OfflineMode = true;
        }

        public void StartLevel()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            LevelHandler.Instance.SetLevelData(0);
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void CreateLobby()
        {
            if (!PhotonNetwork.IsConnected && !PhotonNetwork.InRoom) return;
            string roomId = UnityEngine.Random.Range(1000, 9999).ToString();
            PhotonNetwork.CreateRoom(roomId, new RoomOptions { MaxPlayers = MAX_PLAYERS });
        }

        public void JoinLobby()
        {
            if (!PhotonNetwork.IsConnected && !PhotonNetwork.InRoom) return;
            PhotonNetwork.JoinRoom(roomNameInput.text);
        }

        public void Connect()
        {
            PhotonNetwork.GameVersion = UnityEngine.Application.version;
            PhotonNetwork.NickName = Environment.UserName;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnConnectedToMaster()
        {
            if (!PhotonNetwork.OfflineMode)
            {
                Debug.Log("Connected to master");
                mainState.SetState(MainMenuState.MenuState.Lobby);
            }

            else
            {
                Debug.Log("Connected to master in offline mode");
                PhotonNetwork.CreateRoom("Offline");
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning("Disconnected from master with reason: " + cause);
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.OfflineMode) StartLevel();

            mainState.SetState(MainMenuState.MenuState.Room);
            roomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
        }
    }
}
