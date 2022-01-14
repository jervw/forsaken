using UnityEngine;

public class MainMenuState : MonoBehaviour
{
    public static MainMenuState Instance;

    public GameObject mainView, settingsView, lobbyView, roomView;
    public Sound menuMusic;
    MenuState currentState;

    void Awake()
    {
        Instance = this;
        Cursor.visible = true;
        AudioManager.Instance.Play(menuMusic.clip.name);
    }

    void Start() => SetState(MenuState.Main);

    void UpdateState()
    {
        switch (currentState)
        {
            case MenuState.Main:
                mainView.SetActive(true);
                settingsView.SetActive(false);
                lobbyView.SetActive(false);
                roomView.SetActive(false);
                break;
            case MenuState.Settings:
                mainView.SetActive(true);
                settingsView.SetActive(true);
                lobbyView.SetActive(false);
                roomView.SetActive(false);
                break;
            case MenuState.Lobby:
                mainView.SetActive(false);
                settingsView.SetActive(false);
                lobbyView.SetActive(true);
                roomView.SetActive(false);
                break;
            case MenuState.Room:
                mainView.SetActive(false);
                settingsView.SetActive(false);
                lobbyView.SetActive(false);
                roomView.SetActive(true);
                break;
        }
    }

    public void SetState(MenuState state)
    {
        currentState = state;
        UpdateState();
    }

    public MenuState GetState() => currentState;

    public enum MenuState
    {
        Main,
        Settings,
        Lobby,
        Room,
    }
}
