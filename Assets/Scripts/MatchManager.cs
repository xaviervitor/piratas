using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MatchManager : MonoBehaviour {
    public float MatchTime = 3 * 60f;
    public float EnemySpawnTimer = 0.125f;

    public LayerMask OutOfBoundsLayer;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private List<GameObject> Enemies;

    private float tilemapSize = 30f;
    private int spawnTries = 3;
    
    void Start() {
        StartCoroutine(SpawnEnemies(EnemySpawnTimer));
        StartCoroutine(EndMatchAfterTime(MatchTime));
    }

    IEnumerator SpawnEnemies(float spawnTimer) {
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
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
            enemy.transform.rotation = GetRandomRotation();
            enemy.transform.GetChild(0).GetComponent<Enemy>().Player = Player;
        } 
    }

    IEnumerator EndMatchAfterTime(float matchTime) {
        yield return new WaitForSeconds(matchTime); 
        // matchEnded = true;
    }

    Vector2 GetRandomPosition(float boundaryMinX, float boundaryMaxX, float boundaryMinY, float boundaryMaxY) {
        Vector2 random = new Vector2();
        random.x = Random.Range(boundaryMinX, boundaryMaxX);
        random.y = Random.Range(boundaryMinY, boundaryMaxY);
        return random;
    }

    Quaternion GetRandomRotation() {
        return Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
    }

    bool isValidSpawnPosition(Vector2 randomPosition, Vector2 playerPosition) {
        if (Vector2.Distance(randomPosition, playerPosition) < 8) return false;
        RaycastHit2D result = Physics2D.CircleCast(new Vector2(randomPosition.x - 2.35f, randomPosition.y), 3, Vector2.zero, 0, OutOfBoundsLayer);
        if (result.collider is TilemapCollider2D) return false;
        
        return true;
    }
}
