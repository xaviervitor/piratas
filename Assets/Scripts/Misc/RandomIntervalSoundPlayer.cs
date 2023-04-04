using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIntervalSoundPlayer : MonoBehaviour {
    [SerializeField] private List<AudioSource> Sounds;
    
    void Start() {
        StartCoroutine(PlaySoundsAtRandomIntervals());
    }

    IEnumerator PlaySoundsAtRandomIntervals() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0f, 8f));
            Sounds[Random.Range(0, Sounds.Count)].Play();
        }
    }
}
