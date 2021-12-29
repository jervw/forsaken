using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : MonoBehaviour
{
    public GameObject mainView, lobbyView, roomView;

    MenuState currentState;

    void Awake()
    {
        SetState(MenuState.Main);
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case MenuState.Main:
                mainView.SetActive(true);
                lobbyView.SetActive(false);
                roomView.SetActive(false);
                break;
            case MenuState.Lobby:
                mainView.SetActive(false);
                lobbyView.SetActive(true);
                roomView.SetActive(false);
                break;
            case MenuState.Room:
                mainView.SetActive(false);
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

    public MenuState GetState() { return currentState; }

    public enum MenuState
    {
        Main,
        Lobby,
        Room,
    }
}
