using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayMenuController : MonoBehaviour {
    [SerializeField] private GameObject GameEndMenu;
    [SerializeField] private TMP_Text GameEndTitleText;
    [SerializeField] private TMP_Text GameEndScoreText;
    [SerializeField] private GameObject TimeRemainingText;

    private UpdateMatchTimeUI updateMatchTimeUI;
    
    void OnEnable() {
        MatchManager.GameEndedEvent += OnGameEndedEvent;
    }

    void OnDisable() {
        MatchManager.GameEndedEvent -= OnGameEndedEvent;
    }

    void Start() {
        updateMatchTimeUI = TimeRemainingText.GetComponent<UpdateMatchTimeUI>();
    }

    public void OnPlayAgainButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnMainMenuButtonClick() {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnGameEndedEvent(int currentGameMode, bool playerWon, int enemiesDestroyed) {
        if (currentGameMode == (int) MatchManager.GameMode.TimeLimit) {
            if (playerWon) {
                GameEndTitleText.text = Constants.GameEndTimeLimitPlayerWon;
            } else {
                GameEndTitleText.text = Constants.GameEndTimeLimitPlayerLost;
            }
            GameEndScoreText.text = Constants.GameEndScore + enemiesDestroyed;
        } else {
            GameEndTitleText.text = Constants.GameEndTimeLimitPlayerLost;
            GameEndScoreText.text = Constants.GameEndScore + updateMatchTimeUI.GetMatchTime();
        }

        GameEndMenu.SetActive(true);
    }
}
