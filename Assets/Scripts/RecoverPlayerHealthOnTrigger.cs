using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverPlayerHealthOnTrigger : MonoBehaviour {
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject Particles;
    [SerializeField] private GameObject Sprites;
    
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
        collider.GetComponent<Ship>().ChangeHealth(+1f);
        Destroy(Sprites);
        foreach(Transform particle in Particles.transform) {
            particle.GetComponent<ParticleSystem>().Stop();
        }
        Destroy(Parent, 2f);
    }
}
