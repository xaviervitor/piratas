using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ship Skin", menuName = "Scriptable Objects/Ship Skin")]
public class ShipSkin : ScriptableObject {
    public Color healthWheelColor;
    public List<Sprite> sprites;
}
