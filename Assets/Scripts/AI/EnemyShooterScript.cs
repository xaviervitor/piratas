using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooterScript : Enemy {
    
    [SerializeField]
    private GameObject CannonballPrefab;
    [SerializeField]
    private List<GameObject> CannonballSpawners;
    [SerializeField]
    private float cannonballShotDelay = 1f;
    
    private float cannonballShotTimer;

    protected new void Start() {
        base.Start();
        maxHealth = 2f;
        health = 2f;
        cannonballShotTimer = cannonballShotDelay;
    }

    protected override void AttackStateUpdate() {
        Chase(0f);
        
        if (cannonballShotTimer > 0) {
            cannonballShotTimer -= Time.deltaTime;
        } else {
            cannonballShotTimer = cannonballShotDelay;
            foreach (GameObject cannonballSpawner in CannonballSpawners) {
                Instantiate(CannonballPrefab, cannonballSpawner.transform);
            }
        }
    }
}