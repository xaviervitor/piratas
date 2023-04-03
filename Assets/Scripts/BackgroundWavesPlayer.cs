using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWavesPlayer : MonoBehaviour {
    [SerializeField] private List<AudioSource> Waves;
    
    void Start() {
        StartCoroutine(PlayWavesAtRandomIntervals());
    }

    IEnumerator PlayWavesAtRandomIntervals() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0f, 8f));
            Waves[Random.Range(0, Waves.Count)].Play();
        }
    }
}
