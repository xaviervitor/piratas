using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnTrigger : MonoBehaviour {
    
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private GameObject shipGameObject; 

    void OnTriggerEnter2D(Collider2D collider) {
        // Ship ship = (Ship) collider.gameObject.GetComponent<Ship>();

        // if (ship == null) return;
        
        // Instantiate(Explosion, transform.position, transform.rotation);
        // Ship explodedShip = shipGameObject.GetComponent<Ship>();
        // explodedShip.TakeDamage(explodedShip.maxHealth);
    }
}
