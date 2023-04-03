using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooter : Enemy {
    
    [SerializeField] private GameObject CannonballPrefab;
    [SerializeField] private List<GameObject> CannonballSpawners;
    [SerializeField] private GameObject ShotPrefab;
    [SerializeField] private float cannonballShotDelay = 1f;
    
    private float cannonballShotTimer;
    private int objInstanceID;

    protected new void Start() {
        base.Start();
        cannonballShotTimer = cannonballShotDelay;
        objInstanceID = gameObject.GetInstanceID();
    }

    protected override void IdleStateUpdate() {
        currentAngularVelocity = -AngularVelocity / 2f;
        currentVelocity = Speed / 2f * -transform.up;
    }

    protected override void ChaseStateUpdate() {
        Chase(Speed);
    }
    
    protected override void AttackStateUpdate() {
        Chase(0f);
        if (cannonballShotTimer > 0f) {
            cannonballShotTimer -= Time.deltaTime;
        } else {
            cannonballShotTimer = cannonballShotDelay;
            Cannonball cannonball = null;
            foreach (GameObject cannonballSpawner in CannonballSpawners) {
                cannonball = SpawnCannonball(CannonballPrefab, ShotPrefab, cannonballSpawner.transform, objInstanceID);
                cannonball.playSounds = false;
            }
            if (cannonball != null) {
                cannonball.playSounds = true;
            }
        }
    }
}