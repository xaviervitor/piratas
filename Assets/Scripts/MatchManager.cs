using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class MatchManager : MonoBehaviour {
    public enum GameMode { Infinite, TimeLimit } 
    
    public delegate void EnemySpawnedDelegate(Enemy enemy, Vector2 initialPosition);
    public static event EnemySpawnedDelegate EnemySpawnedEvent;

    public delegate void GameEndedDelegate(int currentGameMode, bool playerWon, int enemiesDestroyed);
    public static event GameEndedDelegate GameEndedEvent;

    [SerializeField] private LayerMask OutOfBoundsLayer;
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GameObject> Enemies;
    [SerializeField] private GameObject GhostEnemy;
    [SerializeField] private Vector2 GhostEnemySpawnPosition = new Vector2(8f, 0f);

    private float tilemapSize = 30f;
    private int spawnTries = 3;
    private int enemiesDestroyed = 0;

    private float MatchTime;
    private float EnemySpawnTimer;
    private int CurrentGameMode;

    private bool matchEnded = false;

    private GameObject enemiesContainer;

    void OnEnable() {
        Player.GetComponent<Ship>().ShipDestroyedEvent += OnPlayerDestroyedEvent;
    }

    void Start() {
        Cursor.visible = false;
        Time.timeScale = 1;

        if (!PlayerPrefs.HasKey(PlayerSettings.MatchTime)) {
            PlayerPrefs.SetFloat(PlayerSettings.MatchTime, PlayerSettings.defaultMatchTime);
        }

        if (!PlayerPrefs.HasKey(PlayerSettings.EnemySpawnTime)) {
            PlayerPrefs.SetFloat(PlayerSettings.EnemySpawnTime, PlayerSettings.defaultEnemySpawnTime);
        }

        if (!PlayerPrefs.HasKey(PlayerSettings.GameMode)) {
            PlayerPrefs.SetInt(PlayerSettings.GameMode, PlayerSettings.defaultGameMode);
        }

        CurrentGameMode = PlayerPrefs.GetInt(PlayerSettings.GameMode);
        MatchTime = PlayerPrefs.GetFloat(PlayerSettings.MatchTime);
        EnemySpawnTimer = PlayerPrefs.GetFloat(PlayerSettings.EnemySpawnTime);
        
        StartCoroutine(SpawnEnemies(EnemySpawnTimer));

        if (CurrentGameMode == (int) GameMode.TimeLimit) {
            StartCoroutine(EndMatchAfterTime(MatchTime));
        }
    }

    IEnumerator SpawnEnemies(float spawnTimer) {
        enemiesContainer = new GameObject("Enemies");
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
            if (Player == null || matchEnded) break;
            
            Vector2 initialPosition = GetRandomPosition(-tilemapSize, tilemapSize, -tilemapSize, tilemapSize);
            bool validSpawn = false;
            for (int i = 0 ; i < spawnTries ; i++) {
                if (isValidSpawnPosition(initialPosition, Player.transform.position)) {
                    validSpawn = true;
                    break;
                }
                initialPosition = GetRandomPosition(-tilemapSize, tilemapSize, -tilemapSize, tilemapSize);
            }
            if (!validSpawn) continue;

            SpawnEnemy(Enemies[Random.Range(0, Enemies.Count)], initialPosition);
        } 
    }

    void SpawnEnemy(GameObject EnemyPrefab, Vector2 initialPosition) {
        GameObject instantiatedEnemy = Instantiate(EnemyPrefab, enemiesContainer.transform);
        instantiatedEnemy.transform.position = initialPosition;
        Enemy enemy = instantiatedEnemy.transform.GetChild(0).GetComponent<Enemy>();
        enemy.Player = Player;
        enemy.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
        
        if (EnemySpawnedEvent != null) {
            EnemySpawnedEvent(enemy, initialPosition);
        }
    }

    IEnumerator EndMatchAfterTime(float matchTime) {
        yield return new WaitForSeconds(matchTime); 
        EndMatch(true);
    }

    void OnPlayerDestroyedEvent() {
        EndMatch(false);
    }

    Vector2 GetRandomPosition(float boundaryMinX, float boundaryMaxX, float boundaryMinY, float boundaryMaxY) {
        Vector2 random = new Vector2();
        random.x = Random.Range(boundaryMinX, boundaryMaxX);
        random.y = Random.Range(boundaryMinY, boundaryMaxY);
        return random;
    }

    bool isValidSpawnPosition(Vector2 randomPosition, Vector2 playerPosition) {
        if (Vector2.Distance(randomPosition, playerPosition) < 8) return false;
        RaycastHit2D result = Physics2D.CircleCast(new Vector2(randomPosition.x - 2.35f, randomPosition.y), 3, Vector2.zero, 0, OutOfBoundsLayer);
        if (result.collider is TilemapCollider2D) return false;
        
        return true;
    }

    void EndMatch(bool playerWon) {
        if (matchEnded) return;
        matchEnded = true;
        Cursor.visible = true;

        if (CurrentGameMode == (int) GameMode.TimeLimit && playerWon) {
            Time.timeScale = 0;
            
        }

        if (GameEndedEvent != null) {
            GameEndedEvent(CurrentGameMode, playerWon, enemiesDestroyed);
        }
    }

    void OnEnemyDestroyedEvent() {
        enemiesDestroyed++;
        if (enemiesDestroyed % 10 == 0) {
            SpawnEnemy(GhostEnemy, GhostEnemySpawnPosition);
        }
    }
}
