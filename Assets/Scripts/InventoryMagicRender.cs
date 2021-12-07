using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMagicRender : MonoBehaviour {
    private InventoryOfMagicPotions _inventory = null;

    private void Start() {
        _inventory = GetComponent<InventoryOfMagicPotions>();
    }
    
    [SerializeField] private string [] arrayKeys = null;

    [SerializeField] private int startX = 0;
    [SerializeField] private int deltaX = 0;
    [SerializeField] private int width = 0;
    [SerializeField] private int heightTop = 0;
    [SerializeField] private int heightBottom = 0;
    [SerializeField] private int yTop = 0;
    [SerializeField] private int yBottom = 0;

    [SerializeField] private bool allowRender = true;
    
    private void OnGUI()
    {
        if (!allowRender)
            return;
        
        if (arrayKeys == null) return;
        if (arrayKeys.Length == 0) return;

        for (int i = 0; i < arrayKeys.Length; i++) {
            // get element
            int index = i;
            string key = arrayKeys[index];
            InventoryMagicPotion element = _inventory.GetByKey(key);

            // horizontal position
            int x = startX + index * (width + deltaX);
            
            // top part of card
            Rect topRect = new Rect(x, yTop, width, heightTop);
            string content = GetCardInfo(element);
            GUI.Box(topRect, content);
            
            // bottom part of card
            Rect bottomRect = new Rect(x, yBottom, width, heightBottom);
            GUI.Box(bottomRect, element.magicPotionScript.potionImage.texture);
        }
    }

    private static string GetCardInfo(InventoryMagicPotion element) {
        string key = "Type: " + element.magicPotionScript.potionKey;
        string magic = "Magic: " + element.magicPotionScript.potionMagic;
        string have = element.numberCurrent + " / " + element.numberMaximum;
        return key + "\n" + magic + "\n" + have;
    }
}
