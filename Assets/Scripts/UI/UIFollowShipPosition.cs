using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowShipPosition : MonoBehaviour {
    [SerializeField] private GameObject Ship;
    
    private RectTransform rectTransform;

    private Vector3 offset;

    void OnEnable() {
        Ship.GetComponent<Ship>().ShipDestroyedEvent += OnShipDestroyedEvent;
    }

    void OnDisable() {
        Ship.GetComponent<Ship>().ShipDestroyedEvent -= OnShipDestroyedEvent;
    }

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        offset = rectTransform.anchoredPosition;
    }

    // void Update() {
    //     rectTransform.anchoredPosition = Ship.transform.localPosition + offset;
    // }

    void LateUpdate() {
        rectTransform.anchoredPosition = Ship.transform.localPosition + offset;
    }

    void OnShipDestroyedEvent() {
        Destroy(gameObject);
    }
}
