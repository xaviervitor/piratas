using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChaserScript : Enemy {
    
    protected new void Start() {
        base.Start();
        health = 1f;
        maxHealth = 2f;
    }
}
