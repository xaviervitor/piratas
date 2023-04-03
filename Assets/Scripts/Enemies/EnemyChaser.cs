using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChaser : Enemy {
    
    [SerializeField] private GameObject Explosion;

    protected new void Start() {
        base.Start();
    }

    protected override void IdleStateUpdate() {
        currentAngularVelocity = AngularVelocity / 2f;
        currentVelocity = Speed / 2f * -transform.up;
    }

    protected override void ChaseStateUpdate() {
        Chase(Speed);
    }

    protected override void AttackStateUpdate() {
        Instantiate(Explosion, transform.position, transform.rotation);
        ChangeHealth(-maxHealth);
    }
}
