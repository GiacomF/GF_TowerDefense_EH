using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GSGamePlay : IGameState
{
    public void OnStateEnter()
    {
        GameManager.instance.LoadLevel(0);
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