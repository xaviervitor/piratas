using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndMenuController : MonoBehaviour {
    public void OnJogarNovamenteButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnVoltarMenuButtonClick() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
