using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorPotionsControl : MonoBehaviour  {
    private GameObject _hero = null;
    private InventoryOfMagicPotions _inventory = null;

    private void Start() {
        _hero = GameObject.Find("Hero");
        _inventory = GameObject.Find("----InventoryManager----").GetComponent<InventoryOfMagicPotions>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject != _hero) return;

        int sum = _inventory.CalculateSumAll();
        string msg = "Collected: " + sum;
        Debug.Log(msg);
        
        _inventory.ZeroCounts();
        
        if (_canChangeMusic) ChangeMusic();
    }

    private bool _canChangeMusic = true;
    
    private void ChangeMusic() {
        // prohibit change
        _canChangeMusic = false;
        
        // stop
        SoundControl funnyMusic = LinkerSounds.DictionaryGetByKey("FunnyMusic");
        if (funnyMusic) funnyMusic.StopSound();

        // play
        SoundControl endMusic = LinkerSounds.DictionaryGetByKey("EndMusic");
        if (endMusic) endMusic.SoundPlayCircle();
    }
}
