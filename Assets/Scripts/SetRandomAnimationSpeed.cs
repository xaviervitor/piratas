using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomAnimationSpeed : MonoBehaviour {
    void Start() {
        GetComponent<Animator>().SetFloat("AnimationSpeed", Random.Range(-1f, 1f));
    }
}
