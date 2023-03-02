using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnIndicatorManager : MonoBehaviour {

    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject SpawnIndicator;
    [SerializeField] private GameObject GhostSpawnIndicator;
    [SerializeField] private float indicatorPadding = 0.5f;

    
    void OnEnable() {
        MatchManager.EnemySpawnedEvent += OnEnemySpawned;
    }

    void OnDisable() {
        MatchManager.EnemySpawnedEvent -= OnEnemySpawned;
    }

    void OnEnemySpawned(Enemy enemy, Vector2 initialPosition) {
        GameObject indicator;
        if (enemy is EnemyGhost) {
            indicator = Instantiate(GhostSpawnIndicator, transform);
        } else {
            indicator = Instantiate(SpawnIndicator, transform);
        }
        Vector2 targetVector = initialPosition - new Vector2(MainCamera.transform.position.x, MainCamera.transform.position.y);
        
        float screenVerticalExtent = MainCamera.orthographicSize;
        float screenHorizontalExtent = screenVerticalExtent * Screen.width / Screen.height;
        indicator.transform.localPosition = targetVector.normalized * (Mathf.Min(screenVerticalExtent, screenHorizontalExtent) - indicatorPadding);
        
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg + 270f;
        indicator.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
