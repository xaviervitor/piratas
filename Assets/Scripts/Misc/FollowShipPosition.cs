using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowShipPosition : MonoBehaviour {
    [SerializeField] public GameObject Ship;
    
    public Vector3 offset;

    void Update() {
        if (Ship == null) return;
        transform.localPosition = Ship.transform.localPosition + offset;
    }
    
    void OnShipDestroyedEvent() {
        Destroy(gameObject);
    }
}
