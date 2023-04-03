using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpawnIndicator : MonoBehaviour {
    public GameObject Enemy;
    public GameObject SpawnIndicatorSpawner;
    
    [SerializeField] private float indicatorPadding = 0.5f;
    [SerializeField] private Vector2 indicatorOffset = new Vector2(0, 1.75f);
    
    private Camera mainCamera;
    private float indicatorMaxDistance;
    private Quaternion indicatorRotation;

    void Start() {
        mainCamera = Camera.main;
        float screenVerticalExtent = mainCamera.orthographicSize;
        float screenHorizontalExtent = screenVerticalExtent * Screen.width / Screen.height;
        indicatorMaxDistance = Mathf.Min(screenVerticalExtent, screenHorizontalExtent) - indicatorPadding;
        indicatorRotation = Quaternion.Euler(0f, 0f, 180f);
        UpdatePositionAndRotation();
    }

    void Update() {
        if (Enemy == null) return;
        UpdatePositionAndRotation();
    }

    void UpdatePositionAndRotation() {
        Vector2 targetVector = (Vector2) Enemy.transform.position - new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
        
        if (targetVector.magnitude > indicatorMaxDistance) {
            transform.SetParent(SpawnIndicatorSpawner.transform);
            float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg + 270f;
            transform.SetLocalPositionAndRotation(
                targetVector.normalized * indicatorMaxDistance,
                Quaternion.AngleAxis(angle, Vector3.forward));
        } else {
            transform.SetParent(Enemy.transform.parent.transform);
            transform.SetLocalPositionAndRotation(
                (Vector2) Enemy.transform.localPosition + indicatorOffset,
                indicatorRotation);
        }
    }
}
