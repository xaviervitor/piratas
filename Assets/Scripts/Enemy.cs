using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Ship {
    [SerializeField]
    public GameObject Player;

    private EnemyStateMachine stateMachine;

    protected new void Start() {
        base.Start();
        stateMachine = GetComponent<EnemyStateMachine>();
        // Event initialization only on Start() because the Player variable 
        // isn't initialized at OnEnable() and is null at OnDisable(). 
        Player.GetComponent<Ship>().ShipDestroyedEvent += OnPlayerDestroyedEvent;
    }

    protected void Update() {
        if (stateMachine.state == EnemyStateMachine.EnemyState.Idle) {
            IdleStateUpdate();
        } else if (stateMachine.state == EnemyStateMachine.EnemyState.Chase) {
            ChaseStateUpdate();
        } else if (stateMachine.state == EnemyStateMachine.EnemyState.Attack) {
            AttackStateUpdate();
        }
    }

    void OnPlayerDestroyedEvent() {
        stateMachine.ChangeState(EnemyStateMachine.EnemyState.Idle);
    }
    
    protected virtual void IdleStateUpdate() {
        currentTorque = Torque / 2;
        currentSpeed = Speed / 2;
    }

    protected virtual void ChaseStateUpdate() {
        Chase(Speed);
    }

    protected virtual void AttackStateUpdate() {
        
    }

    protected void Chase(float speed) {
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Torque / 32 * Time.deltaTime);
        currentTorque = 0;
        currentSpeed = speed;
    }
}
