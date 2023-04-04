using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerSkin : MonoBehaviour {
    [SerializeField] private GameObject HealthWheelSliderFill;
    [SerializeField] private List<ShipSkin> PlayerSkins;

    private Ship ship;
    private SpriteRenderer playerSpriteRenderer;
    private Image sliderFill;

    void Start() {
        ship = GetComponent<Ship>();
        playerSpriteRenderer = ship.GetComponent<SpriteRenderer>();
        sliderFill = HealthWheelSliderFill.GetComponent<Image>();

        SetSkin(PlayerPrefs.GetInt(PlayerSettings.PlayerSkin));
    }

    void SetSkin(int selectedPlayerSkin) {
        ShipSkin playerSkin = PlayerSkins[selectedPlayerSkin];
        ship.ActiveShipSkin = playerSkin;
        sliderFill.color = playerSkin.healthWheelColor;
        playerSpriteRenderer.sprite = playerSkin.sprites[playerSkin.sprites.Count - 1];
    }
}
