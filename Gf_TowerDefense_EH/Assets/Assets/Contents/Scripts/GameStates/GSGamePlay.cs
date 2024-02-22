using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GSGamePlay : IGameState
{
    public void OnStateEnter()
    {
        GameManager.instance.LoadLevel(0);
        UIManager.instance.ShowUI(UIManager.GameUI.GamePlay);
    }

    public void OnStateExit()
    {
        Debug.Log("i'm out");
        GameManager.instance.DestroyLevel();
    }

    public void OnStateUpdate()
    {

    }
}