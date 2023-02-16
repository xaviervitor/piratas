using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {
    public enum EnemyState {
        Idle, 
        Chase,
        Attack
    }
    
    public EnemyState state = EnemyState.Idle;
    
    public void ChangeState(EnemyState newState) {
        state = newState;
    }
}