using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameStateManager>();
                if (_instance == null)
                    Debug.LogError("GameStateManager not found, can't create singleton object");
            }
            return _instance;
        }
    }

    public Dictionary<GameStates, IGameState> registeredGameStates = new Dictionary<GameStates, IGameState>();


    public enum GameStates
    {
        Loading,
        MainMenu,
        GamePlay,
        Win,
        Gameover
    }

    public IGameState currentGameState = null;

    public void RegisterState(GameStates gstate, IGameState state)
    {
        registeredGameStates.Add(gstate, state);
    }

    public void SetCurrentGameState(GameStates gstate)
    {
        if (currentGameState != null)
            currentGameState.OnStateExit();

        IGameState newState = registeredGameStates[gstate];
        newState.OnStateEnter();
        currentGameState = newState;
    }

    private void Update()
    {
        if (currentGameState != null)
            currentGameState.OnStateUpdate();
    }
}
