using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBossControl : MonoBehaviour {
    [SerializeField] private string [] needDeactivateArr = null;
    
    private static void ColorLinesChange(Color color) {
        if (!(FindObjectsOfType(typeof(LineBossControl)) is LineBossControl[] arr)) return;
        if (arr.Length == 0) return;
        foreach (LineBossControl lineBossControl in arr) {
            if (lineBossControl) {
                lineBossControl.SetColor(color);
            }
        }
    }

    public void BossKill() {
        // set dead flag
        Type type = typeof(BossControl);
        BossControl bossControl = (BossControl) FindObjectOfType(type);
        if (bossControl) bossControl.BossDie();

        // change lines color
        Color color = Color.red;
        ColorLinesChange(color);

        // fire elements deactivate
        string [] arr = needDeactivateArr;
        foreach (string key in arr) OneElementDeactivate(key);
    }

    private static void OneElementDeactivate(string key) {
        GameObject elemObj = GameObject.Find(key);
        if (!elemObj) return;
        elemObj.SetActive(false);
    }
}
