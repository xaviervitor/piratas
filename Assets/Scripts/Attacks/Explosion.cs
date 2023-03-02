using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    void Start () {
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Ship ship = (Ship) collider.gameObject.GetComponent<Ship>();

        if (ship != null) {
            ship.ChangeHealth(-1f); 
        }
    }
}
