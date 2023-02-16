using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMatchTimeUI : MonoBehaviour {
    [SerializeField]
    private TMP_Text TimeRemainingText;

    private float matchTime;
    
    private float defaultMatchTime = 2 * 60f;

    void Start() {
        matchTime = PlayerPrefs.GetFloat(PlayerSettings.TempoDuracao, defaultMatchTime);
    }

    void Update() {
        if (matchTime > 0) {
            matchTime -= Time.deltaTime;
        } else {
            matchTime = 0f;
        }
        UpdateUIText();
    }

    void UpdateUIText() {
        TimeRemainingText.text = string.Format("{0:0.00}", matchTime);
    }

    public void StopTimer() {
        enabled = false;
    }
}
