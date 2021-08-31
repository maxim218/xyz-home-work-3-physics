using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOfMagicPotions : MonoBehaviour {
    [SerializeField] private InventoryMagicPotion [] arr = null;

    public InventoryMagicPotion GetByKey(string key) {
        if (arr == null) return default;
        if (arr.Length == 0) return default;
        foreach (InventoryMagicPotion element in arr) {
            if (key == element.magicPotionScript.potionKey) return element;
        }
        return default;
    }

    public void ChangeCountByKey(string key, int delta) {
        for (int i = 0; i < arr.Length; i++) {
            if (key == arr[i].magicPotionScript.potionKey) {
                arr[i].numberCurrent += delta;
            }
        }
    }

    public void ZeroCounts() {
        for (int i = 0; i < arr.Length; i++) arr[i].numberCurrent = 0;
    }

    public int CalculateSumAll() {
        int sum = 0;
        foreach (InventoryMagicPotion element in arr) {
            int count = element.numberCurrent;
            int magic = element.magicPotionScript.potionMagic;
            sum += (count * magic);
        }
        return sum;
    }
}
