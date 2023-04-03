using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayMenuController : MonoBehaviour {
    [SerializeField] private GameObject PauseMenu;
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

    public void OnGameEndedEvent(bool playerWon, int enemiesDestroyed) {
        if (playerWon) {
            GameEndTitleText.text = Constants.GameEndTimeLimitPlayerWon;
        } else {
            GameEndTitleText.text = Constants.GameEndTimeLimitPlayerLost;
        }
        GameEndScoreText.text = Constants.GameEndScore + enemiesDestroyed;

        GameEndMenu.SetActive(true);
    }

    void Start() {
        updateMatchTimeUI = TimeRemainingText.GetComponent<UpdateMatchTimeUI>();
    }

    public void OnGameEndedPlayAgainButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnGameEndedMainMenuButtonClick() {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        PauseMenu.SetActive(true);
    }

    public void OnUnpause(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        PauseMenu.SetActive(false);
    }

    public void OnPauseContinueButtonClick() {
        PauseMenu.SetActive(false);
    }

    public void OnPauseRestartButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnPauseMainMenuButtonClick() {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }
}
