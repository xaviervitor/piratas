using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {
    public int ownerInstanceID;

    [SerializeField] private GameObject toDestroyObject;
    [SerializeField] private GameObject waterSplashPrefab;
    private float Speed = 10;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rigidbody.AddForce(-transform.up * Speed, ForceMode2D.Impulse);
        // StartCoroutine(ActivateCollisionAfterTimer(0.125f));
        StartCoroutine(DeactivateCannonballAfterTimer(1f));
    }
    
    // IEnumerator ActivateCollisionAfterTimer(float time) {
    //     yield return new WaitForSeconds(time);
    //     collider.enabled = true;
    // }

    IEnumerator DeactivateCannonballAfterTimer(float time) {
        yield return new WaitForSeconds(time);
        Instantiate(waterSplashPrefab, transform.position, transform.rotation);
        Destroy(toDestroyObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (ownerInstanceID == collider.gameObject.GetInstanceID()) return;
        Ship ship = (Ship) collider.gameObject.GetComponent<Ship>();
        if (ship != null) {
            ship.ChangeHealth(-0.5f);
            Destroy(toDestroyObject);
        }
    }
}
