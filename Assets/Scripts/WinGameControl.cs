using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameControl : MonoBehaviour {
    private GameObject _hero = null;
    
    private void Start() {
        // get hero
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != _hero) return;
        // scene load
        HeroControl heroControl = _hero.GetComponent<HeroControl>();
        heroControl.NewLevelLoad();
        // delete
        Destroy(gameObject);
    }
}
