using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionControl : MonoBehaviour {
    [SerializeField] private MagicPotionScript _magicPotionScript = null;

    private GameObject _hero = null;
    private InventoryOfMagicPotions _inventory = null;
    
    private void Start() {
        // sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _magicPotionScript.potionImage;

        // hero and his inventory
        _hero = GameObject.Find("Hero");
        _inventory = GameObject.Find("----InventoryManager----").GetComponent<InventoryOfMagicPotions>();
    }

    private string GetMyKey() {
        string key = _magicPotionScript.potionKey;
        return key;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject != _hero) return;
        string key = GetMyKey();
        InventoryMagicPotion invStruct = _inventory.GetByKey(key);
        if (invStruct.numberCurrent >= invStruct.numberMaximum) return;
        const int delta = 1;
        _inventory.ChangeCountByKey(key, delta); 
        Destroy(gameObject);
    }
}
