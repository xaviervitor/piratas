using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGhost : Enemy {
    [SerializeField] private GameObject healthDropPrefab;
    [SerializeField] private float damageStrength = 1 / 64f;
    [SerializeField] private float damageRate = 1f / 16f;
    
    private Ship playerShip;
    private float damageTimer = 0;

    protected new void Start() {
        base.Start();
        damageTimer = damageRate;
        playerShip = Player.GetComponent<Ship>();
    }

    protected override void AttackStateUpdate() {
        Chase(0f);

        if (damageTimer > 0) {
            damageTimer -= Time.deltaTime;
        } else {
            damageTimer = damageRate;
            playerShip.ChangeHealth(-damageStrength);
            ChangeHealth(+damageStrength);
        }
    }

    public override void DestroyShip() {
        base.DestroyShip();
        Instantiate(healthDropPrefab, transform.position, Quaternion.identity);
    }
}