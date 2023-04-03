using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnTrigger : MonoBehaviour {
    [SerializeField] private GameObject ExplosionDamagePrefab;
    [SerializeField] private GameObject DeathExplosionPrefab;
    [SerializeField] private GameObject DestroyAfterTrigger;

    void OnTriggerEnter2D(Collider2D collider) {
        Ship ship = (Ship) collider.gameObject.GetComponent<Ship>();

        if (ship != null) {
            Instantiate(DeathExplosionPrefab, transform.position, transform.rotation);
            Instantiate(ExplosionDamagePrefab, transform.position, transform.rotation);
            Destroy(DestroyAfterTrigger);
        }
    }
}
