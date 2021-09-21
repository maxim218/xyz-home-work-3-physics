using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUiControl : MonoBehaviour {
    [SerializeField] private Color color = Color.white;
    
    private static GameObject ChildGet(GameObject me, string key) {
        GameObject child = me.transform.Find(key).gameObject;
        return child;
    }

    private Image _image = null;
    private Text _text = null;

    private const string keyImage = "Image";
    private const string keyText = "Text";

    private void ChildComponentsGet() {
        _image = ChildGet(gameObject, keyImage).GetComponent<Image>();
        _text = ChildGet(gameObject, keyText).GetComponent<Text>();
    }

    private void SetColor() {
        _image.color = color;
        _text.color = color;
    }

    private InventoryOfMagicPotions _inventory = null;

    private void InventoryGetLink() {
        Type type = typeof(InventoryOfMagicPotions);
        _inventory = (InventoryOfMagicPotions)FindObjectOfType(type);
    }
    
    private void Start() {
        ChildComponentsGet();
        SetColor();
        InventoryGetLink();
    }

    [SerializeField] private string potionTypeColor = string.Empty;
    
    private void LateUpdate() {
        InventoryMagicPotion record = _inventory.GetByKey(potionTypeColor);
        _text.text = "" + record.numberCurrent;
    }
}
