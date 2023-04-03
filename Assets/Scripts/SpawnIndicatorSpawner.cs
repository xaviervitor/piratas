using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicatorSpawner : MonoBehaviour {

    [SerializeField] private GameObject SpawnIndicator;
    [SerializeField] private GameObject GhostSpawnIndicator;

    void OnEnable() {
        EnemySpawner.EnemySpawnedEvent += OnEnemySpawned;
    }

    void OnDisable() {
        EnemySpawner.EnemySpawnedEvent -= OnEnemySpawned;
    }

    void OnEnemySpawned(Enemy enemy, Vector2 initialPosition) {
        GameObject indicator;
        if (enemy is EnemyGhost) {
            indicator = Instantiate(GhostSpawnIndicator, transform);
        } else {
            indicator = Instantiate(SpawnIndicator, transform);
        }
        indicator.GetComponent<UpdateSpawnIndicator>().Enemy = enemy.gameObject;
        indicator.GetComponent<UpdateSpawnIndicator>().SpawnIndicatorSpawner = gameObject;
    }
}
