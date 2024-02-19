using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSWin : IGameState
{
    public void OnStateEnter()
    {
        Debug.Log("Hai vinto");
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }
}