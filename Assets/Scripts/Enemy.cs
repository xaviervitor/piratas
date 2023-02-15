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
    
    protected abstract void IdleStateUpdate();
    protected abstract void ChaseStateUpdate();
    protected abstract void AttackStateUpdate();
}
