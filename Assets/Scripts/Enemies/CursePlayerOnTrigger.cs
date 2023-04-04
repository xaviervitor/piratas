using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursePlayerOnTrigger : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
        PlayerShipController player = collider.GetComponent<PlayerShipController>();
        player.Curse();
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
        PlayerShipController player = collider.GetComponent<PlayerShipController>();
        player.Uncurse();
    }
}
