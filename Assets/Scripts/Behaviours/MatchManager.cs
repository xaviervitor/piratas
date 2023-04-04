using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.InputSystem;

public class MatchManager : MonoBehaviour {
    public delegate void GameEndedDelegate(bool playerWon, int enemiesDestroyed);
    public static event GameEndedDelegate GameEndedEvent;
    
    [SerializeField] private GameObject Player;
    
    private EnemySpawner enemySpawner;
    private int enemiesDestroyed = 0;
    private float MatchTime;
    private int CurrentGameMode;
    private bool matchEnded = false;

    public static void Pause() {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public static void Unpause() {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void OnEnable() {
        Player.GetComponent<Ship>().ShipDestroyedEvent += OnPlayerDestroyedEvent;
        Enemy.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
    }
    
    void OnDisable() {
        Enemy.EnemyDestroyedEvent -= OnEnemyDestroyedEvent;
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

        if (CurrentGameMode == PlayerSettings.GameModes.TimeLimit) {
            StartCoroutine(EndMatchAfterTime(MatchTime));
        }

        enemySpawner = GetComponent<EnemySpawner>();
        enemySpawner.StartSpawner();
    }

    IEnumerator EndMatchAfterTime(float matchTime) {
        yield return new WaitForSeconds(matchTime); 
        EndMatch(true);
    }

    void OnPlayerDestroyedEvent() {
        EndMatch(false);
    }

    void EndMatch(bool playerWon) {
        if (matchEnded) return;
        matchEnded = true;
        enemySpawner.StopSpawner();
        Cursor.visible = true;

        if (CurrentGameMode == PlayerSettings.GameModes.TimeLimit && playerWon) {
            Time.timeScale = 0;
        }

        if (GameEndedEvent != null) {
            GameEndedEvent(playerWon, enemiesDestroyed);
        }
    }

    void OnEnemyDestroyedEvent() {
        enemiesDestroyed++;
        if (enemiesDestroyed % 15 == 0) {
            enemySpawner.SpawnEnemyGhost();
        }
    }
}
