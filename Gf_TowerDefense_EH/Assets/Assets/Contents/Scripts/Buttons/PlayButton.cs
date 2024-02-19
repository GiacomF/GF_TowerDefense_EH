using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void StartGame()
    {
        GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.GamePlay);
    }
}
