using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class MatchManager : MonoBehaviour {
    [SerializeField]
    private LayerMask OutOfBoundsLayer;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private List<GameObject> Enemies;
    [SerializeField]
    private GameObject GameEndMenu;
    [SerializeField]
    private TMP_Text GameEndTituloText;
    [SerializeField]
    private TMP_Text GameEndPontuacaoText;
    [SerializeField]
    private GameObject MatchTimerUI;

    private float tilemapSize = 30f;
    private int spawnTries = 3;
    private int enemiesDestroyed = 0;

    private float MatchTime;
    private float EnemySpawnTimer;
    private float defaultMatchTime = 2 * 60f;
    private float defaultEnemySpawnTimer = 4f;

    private bool matchEnded = false;

    void OnEnable() {
        Player.GetComponent<Ship>().ShipDestroyedEvent += OnPlayerDestroyedEvent;
    }

    void Start() {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey(PlayerSettings.TempoDuracao)) {
            PlayerPrefs.SetFloat(PlayerSettings.TempoDuracao, defaultMatchTime);
        }

        if (!PlayerPrefs.HasKey(PlayerSettings.TempoSpawn)) {
            PlayerPrefs.SetFloat(PlayerSettings.TempoSpawn, defaultEnemySpawnTimer);
        }

        MatchTime = PlayerPrefs.GetFloat(PlayerSettings.TempoDuracao, defaultMatchTime);
        EnemySpawnTimer = PlayerPrefs.GetFloat(PlayerSettings.TempoSpawn, defaultEnemySpawnTimer);
        
        StartCoroutine(SpawnEnemies(EnemySpawnTimer));
        StartCoroutine(EndMatchAfterTime(MatchTime));
    }

    IEnumerator SpawnEnemies(float spawnTimer) {
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
            if (Player == null || matchEnded) break;
            Vector2 randomPosition = GetRandomPosition(-tilemapSize, tilemapSize, -tilemapSize, tilemapSize);
            bool validSpawn = false;
            for (int i = 0 ; i < spawnTries ; i++) {
                if (isValidSpawnPosition(randomPosition, Player.transform.position)) {
                    validSpawn = true;
                    break;
                }
                randomPosition = GetRandomPosition(-tilemapSize, tilemapSize, -tilemapSize, tilemapSize);
            }
            if (!validSpawn) continue;

            GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)]);
            enemy.transform.position = randomPosition;
            Enemy enemyComponent = enemy.transform.GetChild(0).GetComponent<Enemy>();
            enemyComponent.Player = Player;
            enemyComponent.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
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
        if (playerWon) {
            GameEndTituloText.text = "Voce sobreviveu!";
            Time.timeScale = 0;
        } else {
            GameEndTituloText.text = "Voce foi destruido!";
        }
        GameEndPontuacaoText.text = "Pontuacao: " + enemiesDestroyed;

        MatchTimerUI.GetComponent<UpdateMatchTimeUI>().StopTimer();
        GameEndMenu.SetActive(true);
    }

    void OnEnemyDestroyedEvent() {
        enemiesDestroyed++;
    }
}
