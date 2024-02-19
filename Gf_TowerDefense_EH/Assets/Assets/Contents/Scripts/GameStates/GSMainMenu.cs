using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSMainMenu : IGameState
{
    public void OnStateEnter()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.MainMenu);
    }

    public void OnStateExit()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.NONE);
    }

    public void OnStateUpdate()
    {
    }
}
