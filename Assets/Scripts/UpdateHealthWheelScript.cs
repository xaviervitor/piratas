using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthWheelScript : MonoBehaviour {
    [SerializeField]
    private GameObject Ship;
    [SerializeField]
    private GameObject Slider;
    
    private Slider slider;

    void OnEnable() {
        Ship.GetComponent<Ship>().HealthChangedEvent += UpdateHealthWheel;
    }

    void OnDisable() {
        Ship.GetComponent<Ship>().HealthChangedEvent -= UpdateHealthWheel;
    }

    void Start() {
        slider = Slider.GetComponent<Slider>();
    }

    void UpdateHealthWheel(float health, float maxHealth) {
        if (health == 0) {
            Destroy(gameObject);
        }
        slider.value = health / maxHealth;
    }
}
