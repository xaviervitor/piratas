using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private TMP_Dropdown GameModeDropdown;
    [SerializeField] private TMP_InputField MatchTimeInputField;
    [SerializeField] private TMP_InputField EnemySpawnInputField;

    void Start() {
        PlayerPrefs.SetInt(PlayerSettings.GameMode, PlayerSettings.defaultGameMode);
    }

    public void OnJogarButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnConfiguracoesButtonClick() {
        Menu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OnConfiguracoesConfirmarButtonClick() {
        SettingsMenu.SetActive(false);
        Menu.SetActive(true);
        if (MatchTimeInputField.text != "") {
            PlayerPrefs.SetFloat(PlayerSettings.MatchTime, float.Parse(MatchTimeInputField.text));
        }
        if (EnemySpawnInputField.text != "") {
            PlayerPrefs.SetFloat(PlayerSettings.EnemySpawnTime, float.Parse(EnemySpawnInputField.text));
        }
    }

    public void OnGameModeDropdownValueChanged(TMP_Dropdown change) {
        PlayerPrefs.SetInt(PlayerSettings.GameMode, change.value);
        MatchTimeInputField.interactable = (change.value != (int) MatchManager.GameMode.Infinite);
    }
}
