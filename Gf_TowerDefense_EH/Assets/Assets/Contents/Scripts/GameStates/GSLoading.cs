using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSLoading : IGameState
{

    float timer;

    public void OnStateEnter()
    {
        timer = 0;
        UIManager.instance.ShowUI(UIManager.GameUI.Loading);
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {    
        timer += Time.deltaTime;
        if (timer > 3)
            GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.MainMenu);
    }
}
