using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {
    [SerializeField] private float Speed = 45f;

    void Update() {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
