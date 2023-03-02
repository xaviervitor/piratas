using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailFollowShipPosition : MonoBehaviour {
    [SerializeField] private GameObject Ship;
    
    void Update() {
        if (Ship == null) return;
        transform.position = Ship.transform.position;
    }
}
