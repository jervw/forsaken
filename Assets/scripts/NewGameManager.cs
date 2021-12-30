using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class NewGameManager : MonoBehaviourPun
{
    public static NewGameManager Instance { get; private set; }

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }
    public GameObject playerPrefab;

    void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);


    }

    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        Debug.Log("Starting");
        PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);


        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("EnemySpawner", transform.position, Quaternion.identity);
        }
    }
}

[Serializable]
public enum GameState
{
    Starting,
    SpawnEnemies,
    Win,
    Lose,
}