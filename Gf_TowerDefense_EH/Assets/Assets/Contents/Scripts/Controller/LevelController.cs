using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EHLib.Utils;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance;
    public static LevelController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<LevelController>();
                if (_instance == null)
                    Debug.LogError("LevelController not found, can't create singleton object");
            }
            return _instance;
        }
    }

    [System.Serializable]
    public struct WaveInfo
    {
        public GameObject enemy;
        public float enemySpeed;
        public int enemyCount;
        public float spawnTimeout;
    }

    public Transform enemySpawnPoint;
    public Transform waypointsCollector;
    public int EnemyFailCount = 10;
    public List<WaveInfo> levelWaves;

    int currentWave = 0;
    WaveInfo currentWaveInfo;
    int enemyCount;
    bool isSpawning;
    public bool LevelFailedCheck = false;
    float enemyTimeCounter;
    int EscapedEnemy = 0;
    GameObject thisLevel;
    Transform thisLevelTransform;
    GameObject enemy;
    public bool LevelWonCheck = false;
    public int CoinsAvailable = 15;
    public int AlreadySpawnedEnemies;
    public int TotalEnemies;
    

    public void StartLevel()
    {
        TotalEnemies = levelWaves[0].enemyCount + levelWaves[1].enemyCount + levelWaves[2].enemyCount;
        StartWave(levelWaves[0]);
        thisLevel = gameObject;
        thisLevelTransform = gameObject.transform;
    }

    public void StartWave( WaveInfo waveToSpawn)
    {
        enemyCount = 0;
        EscapedEnemy = 0;
        currentWaveInfo = waveToSpawn;
        isSpawning = true;
        enemyTimeCounter = 0;
    }

    void Update()
    {
        Debug.Log(CoinsAvailable);
        Debug.Log(TotalEnemies);
        LevelState();

        if(GameStateManager.instance.currentGameState == GameStateManager.instance.registeredGameStates[GameStateManager.GameStates.GamePlay])
        {
            if (!isSpawning)
                return;

            if (enemyTimeCounter <= 0)
            {
                SpawnEnemy();
                enemyTimeCounter = currentWaveInfo.spawnTimeout;
            }
            
            enemyTimeCounter -= Time.deltaTime;
        }
    }

    public void EnemyEscaped(GameObject enemy)
    {
        EscapedEnemy++;
        GameObject.Destroy(enemy);
    }

    void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(currentWaveInfo.enemy);
        enemy.transform.SetParent(thisLevelTransform);
        enemy.transform.position = enemySpawnPoint.position;

        PathMover enemyMover = enemy.GetComponent<PathMover>();

        Transform[] path = new Transform[waypointsCollector.childCount];

        for (int currentChild = 0; currentChild < waypointsCollector.childCount; currentChild++)
            path[currentChild] = waypointsCollector.GetChild(currentChild);

        enemyMover.pathPoints = path;
        enemyMover.movingSpeed = currentWaveInfo.enemySpeed;
        enemyMover.stopRadiusSqr = 0.3f;

        enemyCount++;
        AlreadySpawnedEnemies++;
        if (enemyCount == currentWaveInfo.enemyCount)
        {
            isSpawning = false;
        } 
    }

    void LevelState()
    {
        enemy = GameObject.FindWithTag("Enemy");
        Debug.Log(currentWave);
        Debug.Log(levelWaves.Count);

        if(enemyCount == currentWaveInfo.enemyCount && currentWave < levelWaves.Count - 1)
        {
            currentWave++;
            Debug.Log("One more wave!");
            StartWave(levelWaves[currentWave]);
        } 
        if(enemy != null)
        {
            Debug.Log("There is at least one enemy on screen");
        }
        if(enemy == null && currentWave == levelWaves.Count - 1 || LevelWonCheck)
        {
            LevelWonCheck = false;
            Debug.Log("Enemies are over!");
            GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Win);
        }
        if(EscapedEnemy >= EnemyFailCount  ||  LevelFailedCheck)
        {
            LevelFailedCheck = false;
            GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Gameover);
        }
    }

    public void AddCoins()
    {
        CoinsAvailable += 5;
        Debug.Log("More Coins!");
    }

    public void DestroyLevel()
    {
        Destroy(thisLevel);
    }
}
