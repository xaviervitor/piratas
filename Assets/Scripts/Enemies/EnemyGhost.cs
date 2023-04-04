using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGhost : Enemy {
    [SerializeField] private GameObject healthDropPrefab;
    [SerializeField] private float damageStrength = 1f / 32f;
    [SerializeField] private float damageRate = 1f / 16f;
    
    private Ship playerShip;
    private float damageTimer = 0f;


    protected new void Start() {
        base.Start();
        damageTimer = damageRate;
        smoothingAccelStep = 1f / 16f;
        smoothingDecelStep = 1f / 64f;
        playerShip = Player.GetComponent<Ship>();
    }

    protected override void IdleStateUpdate() {
        currentAngularVelocity = AngularVelocity / 2f;
        currentVelocity = Speed / 2f * -transform.up;
    }

    protected override void ChaseStateUpdate() {
        Chase(Speed);

        if (damageTimer > 0f) {
            damageTimer -= Time.deltaTime;
        } else {
            damageTimer = damageRate;
            playerShip.ChangeHealth(-damageStrength / 2);
            ChangeHealth(damageStrength / 2);
        }
    }
    
    protected override void AttackStateUpdate() {
        Chase(0f);

        if (damageTimer > 0f) {
            damageTimer -= Time.deltaTime;
        } else {
            damageTimer = damageRate;
            playerShip.ChangeHealth(-damageStrength);
            ChangeHealth(damageStrength);
        }
    }

    public override void DestroyShip() {
        base.DestroyShip();
        Instantiate(healthDropPrefab, transform.position, Quaternion.identity);
    }
}