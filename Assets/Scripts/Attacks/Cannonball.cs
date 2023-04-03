using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {
    public int ownerInstanceID;
    public bool playSounds = true;

    [SerializeField] private GameObject toDestroyObject;
    [SerializeField] private GameObject waterSplashPrefab;

    [SerializeField] private GameObject cannonballFireSoundPrefab;
    [SerializeField] private GameObject waterSplashSoundPrefab;

    private float Speed = 10f;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rigidbody.AddForce(-transform.up * Speed, ForceMode2D.Impulse);
        StartCoroutine(DeactivateCannonballAfterTimer(1f));
        PlayRandomizePitch(cannonballFireSoundPrefab);
    }

    IEnumerator DeactivateCannonballAfterTimer(float time) {
        yield return new WaitForSeconds(time);
        Instantiate(waterSplashPrefab, transform.position, transform.rotation);
        PlayRandomizePitch(waterSplashSoundPrefab);
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

    void PlayRandomizePitch(GameObject soundPrefab) {
        if (!playSounds) return;
        GameObject instantiatedSound = Instantiate(soundPrefab, transform.position, transform.rotation);
        instantiatedSound.GetComponent<AudioSource>().pitch = Random.Range(1f, 2.5f);
    }
}
