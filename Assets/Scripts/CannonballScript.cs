using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour {
    [SerializeField]
    private GameObject parent;
    private float Speed = 10;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rigidbody.AddForce(-transform.up * Speed, ForceMode2D.Impulse);
        StartCoroutine(ActivateCollisionAfterTimer(0.125f));
        Destroy(parent, 1f);
    }
    
     IEnumerator ActivateCollisionAfterTimer(float time) {
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Ship ship = (Ship) collider.gameObject.GetComponent<Ship>();

        if (ship != null) {
            ship.TakeDamage(0.5f);
            Destroy(parent);
        }
    }
}
