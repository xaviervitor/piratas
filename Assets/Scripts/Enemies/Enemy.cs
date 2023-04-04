using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Ship {
    [SerializeField] public GameObject Player;

    public delegate void EnemyDestroyedDelegate();
    public static event EnemyDestroyedDelegate EnemyDestroyedEvent;

    protected float smoothingAccelStep = 1f / 16f;
    protected float smoothingDecelStep = 1f / 256f;

    private EnemyStateMachine stateMachine;
    private float beforeSpeed; 

    protected new void Start() {
        base.Start();
        beforeSpeed = 0f;
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
        
        if (beforeSpeed > speed) {
            float apply = beforeSpeed - smoothingDecelStep;
            currentVelocity = -transform.up * apply;
            beforeSpeed = apply;
        } else if (beforeSpeed < speed) {
            float apply = beforeSpeed + smoothingAccelStep;
            currentVelocity = -transform.up * apply;
            beforeSpeed = apply;
        } else {
            currentVelocity = -transform.up * speed;
        }
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
