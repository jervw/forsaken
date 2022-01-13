using System;
using System.Collections;
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

        public TMP_Text roomName, playerList;
        public TMP_InputField roomNameInput;

        void Awake()
        {
            Instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start() => MainMenuState.Instance.SetState(MainMenuState.MenuState.Main);

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
            if (MainMenuState.Instance.GetState() == MainMenuState.MenuState.Settings) return;

            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();

            PhotonNetwork.OfflineMode = true;
        }

        public void OpenSettingsMenu() => MainMenuState.Instance.SetState(MainMenuState.MenuState.Settings);

        public void StartLevel()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
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
            if (MainMenuState.Instance.GetState() == MainMenuState.MenuState.Settings) return;

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
                MainMenuState.Instance.SetState(MainMenuState.MenuState.Lobby);
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

            MainMenuState.Instance.SetState(MainMenuState.MenuState.Room);
            roomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) => UpdatePlayerList();

        public override void OnPlayerLeftRoom(Player otherPlayer) => UpdatePlayerList();

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) => UpdatePlayerList();
    }
}
