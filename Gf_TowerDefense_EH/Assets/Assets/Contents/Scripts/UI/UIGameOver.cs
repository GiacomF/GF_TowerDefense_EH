using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour, IGameUI
{
    public UIManager.GameUI UIType;

    public UIManager.GameUI GetUIType()
    {
        return UIType;
    }

    public void Init()
    {
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
