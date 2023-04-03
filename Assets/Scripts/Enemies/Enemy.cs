using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Ship {
    [SerializeField] public GameObject Player;

    public delegate void EnemyDestroyedDelegate();
    public static event EnemyDestroyedDelegate EnemyDestroyedEvent;

    private EnemyStateMachine stateMachine;

    protected new void Start() {
        base.Start();
        stateMachine = GetComponent<EnemyStateMachine>();        
    }

    protected new void Update() {
        if (stateMachine.state == EnemyStateMachine.EnemyState.Idle) {
            IdleStateUpdate();
        } else if (stateMachine.state == EnemyStateMachine.EnemyState.Chase) {
            ChaseStateUpdate();
        } else if (stateMachine.state == EnemyStateMachine.EnemyState.Attack) {
            AttackStateUpdate();
        }
    }
    
    protected new void FixedUpdate() {
        rigidbody.angularVelocity = currentAngularVelocity;
        rigidbody.velocity = currentVelocity;
    }

    protected virtual void IdleStateUpdate() {
    }

    protected virtual void ChaseStateUpdate() {        
    }

    protected virtual void AttackStateUpdate() {
    }

    protected void Chase(float speed) {
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        float angleAdjusmentAmount = transform.rotation.eulerAngles.z - rotation.eulerAngles.z;
        if (angleAdjusmentAmount >= 180f) {
            angleAdjusmentAmount -= 360f;
        }
        if (angleAdjusmentAmount <= -180f) {
            angleAdjusmentAmount += 360f;
        }
        float turnDirection = (angleAdjusmentAmount > 0f) ? -1 : 1;

        float correctionAngleLimit = 5f;
        float factor = Mathf.InverseLerp(0f, correctionAngleLimit, Mathf.Abs(angleAdjusmentAmount)); 
        currentAngularVelocity = AngularVelocity * factor * turnDirection;
        
        currentVelocity = -transform.up * speed;
    }

    public void OnPlayerDestroyedEvent() {
        stateMachine.ChangeState(EnemyStateMachine.EnemyState.Idle);
    }

    public override void DestroyShip() {
        base.DestroyShip();
        if (EnemyDestroyedEvent != null) {
            EnemyDestroyedEvent();
        }
    }
}
