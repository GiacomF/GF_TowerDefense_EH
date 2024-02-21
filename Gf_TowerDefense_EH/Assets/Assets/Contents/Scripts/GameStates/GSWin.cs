using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSWin : IGameState
{
    float timer;
    public void OnStateEnter()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Win);
        Debug.Log("Hai vinto");
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Loading);
        }
    }
}