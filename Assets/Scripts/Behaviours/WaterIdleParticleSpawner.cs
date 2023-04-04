using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIdleParticleSpawner : MonoBehaviour {

    [SerializeField] private GameObject waterIdleParticleSystemPrefab;
    
    void Start() {
        StartCoroutine(SpawnWaterIfIdleAfterRandomTime());
    }

    IEnumerator SpawnWaterIfIdleAfterRandomTime() {
        // This is a cheap idle animation timer and has the limitation 
        // of not detecting if the player moved and returned to the
        // same position. Reminder to keep an eye out for movement
        // cycles, like patrolling systems.
        while (true) {
            Vector3 position = transform.position;
            yield return new WaitForSeconds(Random.Range(4f, 10f));
            if (position == transform.position) {
                Instantiate(waterIdleParticleSystemPrefab, transform.position, transform.rotation);
            }
        }
    }
}
