using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameObject ShipGraphic;
    [SerializeField] private List<ShipSkin> ShipSkins;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject CreditsSection;
    [SerializeField] private TMP_Dropdown GameModeDropdown;
    [SerializeField] private TMP_InputField MatchTimeInputField;
    [SerializeField] private TMP_InputField EnemySpawnInputField;

    private Image shipGraphicImage;
    private int currentShipGraphic;

    void Start() {
        if (!PlayerPrefs.HasKey(PlayerSettings.PlayerSkin)) {
            PlayerPrefs.SetInt(PlayerSettings.PlayerSkin, PlayerSettings.defaultPlayerSkin);
        }
        currentShipGraphic = PlayerPrefs.GetInt(PlayerSettings.PlayerSkin);
        shipGraphicImage = ShipGraphic.GetComponent<Image>();
        UpdateShipGraphic();
        GameModeDropdown.value = PlayerPrefs.GetInt(PlayerSettings.GameMode, PlayerSettings.defaultGameMode);
        MatchTimeInputField.text = PlayerPrefs.GetFloat(PlayerSettings.MatchTime, PlayerSettings.defaultMatchTime).ToString();
        EnemySpawnInputField.text = PlayerPrefs.GetFloat(PlayerSettings.EnemySpawnTime, PlayerSettings.defaultEnemySpawnTime).ToString();
    }

    public void OnShipChooserBackButtonClick() {
        currentShipGraphic--;
        if (currentShipGraphic < 0) {
            currentShipGraphic = ShipSkins.Count - 1;
        }
        UpdateShipGraphic();
    }

    public void OnShipChooserForwardButtonClick() {
        currentShipGraphic++;
        if (currentShipGraphic > ShipSkins.Count - 1) {
            currentShipGraphic = 0;
        }
        UpdateShipGraphic();
    }

    public void UpdateShipGraphic() {
        shipGraphicImage.sprite = ShipSkins[currentShipGraphic].sprites[ShipSkins[currentShipGraphic].sprites.Count - 1];
        PlayerPrefs.SetInt(PlayerSettings.PlayerSkin, currentShipGraphic);
    }

    public void OnPlayButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnSettingsButtonClick() {
        Menu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OnSettingsOKButtonClick() {
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
        MatchTimeInputField.interactable = (change.value != (int) PlayerSettings.GameModes.Infinite);
    }

    public void OnCreditsButtonClick() {
        Menu.SetActive(false);
        CreditsSection.SetActive(true);
    }

    public void OnCreditsCloseButtonClick() {
        Menu.SetActive(true);
        CreditsSection.SetActive(false);
    }

    public void OnQuitButtonClick() {
        Application.Quit();
    }
}
