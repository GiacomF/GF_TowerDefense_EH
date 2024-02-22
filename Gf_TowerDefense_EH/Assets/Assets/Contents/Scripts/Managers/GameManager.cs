
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<LevelController> levels;
    public TMP_Text coinsAvailable;
    public TMP_Text SpawnedEnemiesNumber;
    public TMP_Text TotalEnemiesNumber;

    //1 Make it a singleton
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                    Debug.LogError("GameManager not found, can't create singleton object");
            }
            return _instance;
        }
    }
    //

    //2 Create a curtain menu that lets you select which one is the starting state 
    public GameStateManager.GameStates startingState;

    //3 Tells the GameStateManager to execute RegisterSate(), and assigns each enum the correct IGameState(GS)
    private void Awake()
    {
        GameStateManager.instance.RegisterState(GameStateManager.GameStates.Loading, new GSLoading());
        GameStateManager.instance.RegisterState(GameStateManager.GameStates.MainMenu, new GSMainMenu());
        GameStateManager.instance.RegisterState(GameStateManager.GameStates.GamePlay, new GSGamePlay());
        GameStateManager.instance.RegisterState(GameStateManager.GameStates.Win, new GSWin());
        GameStateManager.instance.RegisterState(GameStateManager.GameStates.Gameover, new GSGameOver());
    }

    //4Sets the startingState in GameStateManager as the one you selected in the curtain menu
    private void Start()
    {
        GameStateManager.instance.SetCurrentGameState(startingState);
    }

    //Called by GSGameplay, instantiates the levels[number assigned in GSGamePlay], takes it to worldtransform.position.zero, grabs the LevelController from the Level.GameObject, tells the level controller to execute StartLevel() 
    public void LoadLevel(int levelNum)
    {
        GameObject levelGameObject = GameObject.Instantiate(levels[levelNum].gameObject);
        levelGameObject.transform.position = Vector3.zero;
        LevelController cnt = levelGameObject.GetComponent<LevelController>();
        cnt.StartLevel();
    }

    void Update()
    {
        Debug.Log(GameStateManager.instance.currentGameState);
        if(GameObject.FindAnyObjectByType<LevelController>() != null)
        {
            coinsAvailable.text = LevelController.instance.CoinsAvailable.ToString();
            SpawnedEnemiesNumber.text = LevelController.instance.AlreadySpawnedEnemies.ToString();
            TotalEnemiesNumber.text = LevelController.instance.TotalEnemies.ToString();
        }
    }

    public void DestroyLevel()
    {
        LevelController.instance.DestroyLevel();
    }    
}