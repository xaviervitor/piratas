using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooter : Enemy {
    
    [SerializeField] private GameObject CannonballPrefab;
    [SerializeField] private List<GameObject> CannonballSpawners;
    [SerializeField] private float cannonballShotDelay = 1f;
    
    private float cannonballShotTimer;
    private int objInstanceID;

    protected new void Start() {
        base.Start();
        cannonballShotTimer = cannonballShotDelay;
        objInstanceID = gameObject.GetInstanceID();
    }

    protected override void AttackStateUpdate() {
        Chase(0f);
        
        if (cannonballShotTimer > 0) {
            cannonballShotTimer -= Time.deltaTime;
        } else {
            cannonballShotTimer = cannonballShotDelay;
            foreach (GameObject cannonballSpawner in CannonballSpawners) {
                GameObject instantiatedCannonball = Instantiate(CannonballPrefab, cannonballSpawner.transform.position, cannonballSpawner.transform.rotation);
                Cannonball cannonball = (Cannonball) instantiatedCannonball.transform.GetChild(0).GetComponent<Cannonball>();
                cannonball.ownerInstanceID = objInstanceID;
            }
        }
    }
}