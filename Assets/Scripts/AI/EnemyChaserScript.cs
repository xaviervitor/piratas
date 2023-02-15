using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChaserScript : Enemy {
    
    protected new void Start() {
        base.Start();
        health = 1;
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
        // No attack state for the chaser enemy
    }
}
