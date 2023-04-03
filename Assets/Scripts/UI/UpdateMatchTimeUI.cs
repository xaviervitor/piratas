using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMatchTimeUI : MonoBehaviour {
    [SerializeField] private TMP_Text TimeRemainingText;

    private int currentGameMode;
    private float matchTime;
    
    void OnEnable() {
        MatchManager.GameEndedEvent += OnGameEndedEvent;
    }

    void OnDisable() {
        MatchManager.GameEndedEvent -= OnGameEndedEvent;
    }

    void Start() {
        currentGameMode = PlayerPrefs.GetInt(PlayerSettings.GameMode, PlayerSettings.defaultGameMode);
        if (currentGameMode == PlayerSettings.GameModes.Infinite) {
            matchTime = 0f;
        } else {
            matchTime = PlayerPrefs.GetFloat(PlayerSettings.MatchTime, PlayerSettings.defaultMatchTime);
        }
    }

    void Update() {
        if (currentGameMode == PlayerSettings.GameModes.Infinite) {
            matchTime += Time.deltaTime;
        } else {
            if (matchTime > 0f) {
                matchTime -= Time.deltaTime;
            } else {
                matchTime = 0f;
            }
        }
        UpdateUIText();
    }

    void UpdateUIText() {
        TimeRemainingText.text = string.Format("{0:0.00}", matchTime);
    }

    public string GetMatchTime() {
        return TimeRemainingText.text;
    }

    public void OnGameEndedEvent(bool playerWon, int enemiesDestroyed) {
        enabled = false;
    }
}
