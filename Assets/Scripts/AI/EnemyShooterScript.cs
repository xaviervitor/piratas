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
        health = 2f;
        cannonballShotTimer = cannonballShotDelay;
    }

    protected override void IdleStateUpdate() {
        currentTorque = Torque / 2;
        currentSpeed = Speed / 2;
    }

    protected override void ChaseStateUpdate() {
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Torque / 32 * Time.deltaTime);
        currentTorque = 0;
        currentSpeed = Speed;
    }

    protected override void AttackStateUpdate() {
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Torque / 32 * Time.deltaTime);
        currentTorque = 0;
        currentSpeed = 0;
        
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