using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }
    [SerializeField] GameObject pauseMenu, winMenu, loseMenu;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() => ChangeState(GameState.Starting);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (State == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (State == GameState.Paused)
                ChangeState(GameState.Playing);
        }
    }

    public void ChangeState(GameState newState)
    {

        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Paused:
                HandlePause();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"STATE: {newState}");
    }

    private void HandleStarting()
    {
        // instantiate enemy spawner here TODO

        ChangeState(GameState.Playing);
    }

    private void HandlePlaying()
    {
        Time.timeScale = 1;
        AudioManager.Instance.Play(LevelHandler.Instance.current.music.clip.name); ;
        pauseMenu.SetActive(false);
    }

    private void HandlePause()
    {
        Time.timeScale = 0;
        AudioManager.Instance.Pause(LevelHandler.Instance.current.music.clip.name);
        pauseMenu.SetActive(true);
    }

    private void HandleWin()
    {
        AudioManager.Instance.Pause(LevelHandler.Instance.current.music.clip.name);
        winMenu.SetActive(true);
    }

    private void HandleLose()
    {
        AudioManager.Instance.Pause(LevelHandler.Instance.current.music.clip.name);
        loseMenu.SetActive(true);
    }

    [Serializable]
    public enum GameState
    {
        Starting,
        Playing,
        Paused,
        Win,
        Lose,
    }

}
