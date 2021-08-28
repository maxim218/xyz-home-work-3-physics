using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfGuns : MonoBehaviour {
    [SerializeField] private string keyA = string.Empty;
    [SerializeField] private string keyB = string.Empty;
    [SerializeField] private string keyC = string.Empty;

    private static void SetGunFireFlag(string key, bool flag) {
        GameObject gun = GameObject.Find(key);
        if (!gun) return;
        
        GunControl script = gun.GetComponent<GunControl>();
        if (!script) return;

        script.AllowFireSet(flag);
    }

    private void ProhibitAll() {
        SetGunFireFlag(keyA, false);
        SetGunFireFlag(keyB, false);
        SetGunFireFlag(keyC, false);
    }

    private void Start() {
        ProhibitAll();
        StartCoroutine( AsyncCounting() );
    }

    [SerializeField] private bool circleFlag = true;
    [SerializeField] private float waitValue = 0;
    
    private IEnumerator AsyncCounting() {
        while (circleFlag) {
            yield return new WaitForSeconds(waitValue);
            ProhibitAll();
            _count += 1;
            _count %= 3;
            string key = GetKey(_count);
            SetGunFireFlag(key, true);
        }
    }

    private int _count = 0;

    private string GetKey(int countValue) {
        switch (countValue) {
            case 0: return keyA;
            case 1: return keyB;
            default: return keyC;
        }
    }
}
