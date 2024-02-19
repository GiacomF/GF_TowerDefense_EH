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
    

    public void StartLevel()
    {
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

        if(enemyCount == currentWaveInfo.enemyCount)
        {
            currentWave++;
            if(currentWave == levelWaves.Count)
            {
                isSpawning = false;
                GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Win);
                return;
            }
            StartWave(levelWaves[currentWave]);
        }
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
        if (enemyCount == currentWaveInfo.enemyCount)
        {
            isSpawning = false;

        }
            
    }

    public void LevelFailed()
    {
        LevelFailedCheck = false;
        GameStateManager.instance.SetCurrentGameState(GameStateManager.GameStates.Gameover);
    }

    public void DestroyLevel()
    {
        Destroy(thisLevel);
    }
}
