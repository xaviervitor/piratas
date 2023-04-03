using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateEnemiesDestroyedUI : MonoBehaviour {
    [SerializeField] private TMP_Text EnemiesDestroyedText;

    private int enemiesDestroyed = 0;

    void OnEnable() {
        Enemy.EnemyDestroyedEvent += OnEnemyDestroyedEvent;
    }

    void OnDisable() {
        Enemy.EnemyDestroyedEvent -= OnEnemyDestroyedEvent;
    }

    void Start() {
        UpdateUIText();    
    }

    void UpdateUIText() {
        EnemiesDestroyedText.text = string.Format("{0}", enemiesDestroyed);
    }

    void OnEnemyDestroyedEvent() {
        enemiesDestroyed++;
        UpdateUIText();
    }
}
