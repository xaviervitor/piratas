using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField]
    private GameObject ConfiguracoesMenu;
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private TMP_InputField DuracaoInputField;
    [SerializeField]
    private TMP_InputField TempoSpawnInputField;
    
    void Start() {
        float duracao = PlayerPrefs.GetFloat(PlayerSettings.TempoDuracao, 0f);
        float tempoSpawn = PlayerPrefs.GetFloat(PlayerSettings.TempoSpawn, 0f);
        if (duracao == 0) {
            DuracaoInputField.text = "";
        }
        if (tempoSpawn == 0) {
            TempoSpawnInputField.text = "";
        }
    }

    public void OnJogarButtonClick() {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnConfiguracoesButtonClick() {
        Menu.SetActive(false);
        ConfiguracoesMenu.SetActive(true);
    }

    public void OnConfiguracoesConfirmarButtonClick() {
        ConfiguracoesMenu.SetActive(false);
        Menu.SetActive(true);
        if (DuracaoInputField.text != "") {
            PlayerPrefs.SetFloat(PlayerSettings.TempoDuracao, float.Parse(DuracaoInputField.text));
        }
        if (TempoSpawnInputField.text != "") {
            PlayerPrefs.SetFloat(PlayerSettings.TempoSpawn, float.Parse(TempoSpawnInputField.text));
        }
    }
}
