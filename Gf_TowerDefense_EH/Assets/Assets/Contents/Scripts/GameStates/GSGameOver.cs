using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSGameOver : IGameState
{
    float timer;
    public void OnStateEnter()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Gameover);

        Debug.Log("I entered");
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            Debug.Log("Timer over");
            timer = 0;
            GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Loading);
        }
    }
}