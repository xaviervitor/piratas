using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class EnemySpawner : MonoBehaviour {
    public enum GameMode { Infinite, TimeLimit } 
    
    public delegate void EnemySpawnedDelegate(Enemy enemy, Vector2 initialPosition);
    public static event EnemySpawnedDelegate EnemySpawnedEvent;

    [SerializeField] private LayerMask OutOfBoundsLayer;
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GameObject> Enemies;
    [SerializeField] private GameObject GhostEnemyPrefab;
    [SerializeField] private List<Vector2> GhostEnemySpawnPositions = new List<Vector2>();

    private float tilemapSize = 30f;
    private int spawnTries = 10;

    private Coroutine spawnEnemiesCoroutine;
    
    public void StartSpawner() {
        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies(PlayerPrefs.GetFloat(PlayerSettings.EnemySpawnTime)));
    }

    public void StopSpawner() {
        if (spawnEnemiesCoroutine != null) {
            StopCoroutine(spawnEnemiesCoroutine);
        }
    }

    IEnumerator SpawnEnemies(float spawnTimer) {
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
            
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

    public void SpawnEnemy(GameObject EnemyPrefab, Vector2 initialPosition) {
        GameObject instantiatedEnemy = Instantiate(EnemyPrefab);
        Enemy enemy = instantiatedEnemy.transform.GetChild(0).GetComponent<Enemy>();
        instantiatedEnemy.transform.position = initialPosition;
        if (enemy) {
            enemy.Player = Player;
            Player.GetComponent<Ship>().ShipDestroyedEvent += enemy.OnPlayerDestroyedEvent;
        
            if (EnemySpawnedEvent != null) {
                EnemySpawnedEvent(enemy, initialPosition);
            }
        }
    }

    public void SpawnEnemyGhost() {
        SpawnEnemy(GhostEnemyPrefab, GhostEnemySpawnPositions[Random.Range(0, GhostEnemySpawnPositions.Count)]);
    }

    public void SpawnInGhostPositions(GameObject prefab) {
        SpawnEnemy(prefab, GhostEnemySpawnPositions[Random.Range(0, GhostEnemySpawnPositions.Count)]);
    }

    Vector2 GetRandomPosition(float boundaryMinX, float boundaryMaxX, float boundaryMinY, float boundaryMaxY) {
        Vector2 random = new Vector2();
        random.x = Random.Range(boundaryMinX, boundaryMaxX);
        random.y = Random.Range(boundaryMinY, boundaryMaxY);
        return random;
    }

    bool isValidSpawnPosition(Vector2 randomPosition, Vector2 playerPosition) {
        if (Vector2.Distance(randomPosition, playerPosition) < 6f) return false;
        // Checks if a radius of the spawn position collides with 
        // any obstacle, with an offset to account for its idle circular
        // trajectory.
        RaycastHit2D result = Physics2D.CircleCast(new Vector2(randomPosition.x - 2.35f, randomPosition.y), 3f, Vector2.zero, 0f, OutOfBoundsLayer);
        if (result.collider is TilemapCollider2D) return false;
        
        return true;
    }

    void OnDrawGizmosSelected() {
        foreach (Vector2 position in GhostEnemySpawnPositions) {
            Vector2 center;
            center.x = position.x - 4f; 
            center.y = position.y;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center, 4f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}
