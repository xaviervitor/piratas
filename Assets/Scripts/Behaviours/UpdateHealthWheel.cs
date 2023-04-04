using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthWheel : MonoBehaviour {
    [SerializeField] private GameObject Ship;
    [SerializeField] private GameObject Slider;
    
    private Slider slider;

    void OnEnable() {
        Ship.GetComponent<Ship>().HealthChangedEvent += UpdateHealthWheelUI;
    }

    void OnDisable() {
        Ship.GetComponent<Ship>().HealthChangedEvent -= UpdateHealthWheelUI;
    }

    void Start() {
        slider = Slider.GetComponent<Slider>();
    }

    void UpdateHealthWheelUI(float health, float maxHealth) {
        if (health <= 0f) {
            Destroy(gameObject);
        }
        slider.value = health / maxHealth;
    }
}
