using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChaser : Enemy {
    
    [SerializeField]
    private GameObject Explosion;

    protected new void Start() {
        base.Start();
        health = 1f;
        maxHealth = 2f;
    }

    protected override void AttackStateUpdate() {
        Instantiate(Explosion, transform.position, transform.rotation);
        TakeDamage(maxHealth);
    }
}
