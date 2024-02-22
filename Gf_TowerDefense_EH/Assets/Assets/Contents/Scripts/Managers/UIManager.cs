using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<UIManager>();
                if (_instance == null)
                    Debug.LogError("UIManager not found, can't create singleton object");
            }
            return _instance;
        }
    }

    public enum GameUI
    {
        NONE,
        Loading,
        MainMenu,
        Options,
        Gameover,
        Win,
        GamePlay
    }

    private Dictionary<GameUI, IGameUI> registeredUIs = new Dictionary<GameUI, IGameUI>();
    
    //UIContainer is the canvas
    public Transform UIContainer;

    //Add to the dictionary each UI paired with its interface
    public void RegisterUI( GameUI uiType, IGameUI uiToRegister )
    {
        registeredUIs.Add(uiType, uiToRegister);
    }

    private void Awake()
    {
        //for each 
        foreach (IGameUI enumeratedUI in UIContainer.GetComponentsInChildren<IGameUI>(true))
        {
            RegisterUI(enumeratedUI.GetUIType(), enumeratedUI);
        }

        ShowUI(GameUI.NONE);
    }

    //GameUI uiType is stated every time the ShowUI method is called, for example ShowUI(UIManager.GameUI.Loading)
    public void ShowUI(GameUI uiType)
    {
        foreach( KeyValuePair<GameUI,IGameUI> kvp in registeredUIs)
        {
            kvp.Value.SetActive(kvp.Key == uiType);
        }
    }
}
