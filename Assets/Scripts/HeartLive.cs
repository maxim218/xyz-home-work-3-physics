using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLive : MonoBehaviour {
    private void OnDestroy() {
        HeroHealthControl heroHealthControl = _hero.GetComponent<HeroHealthControl>();
        heroHealthControl.AddThreeLives();
    }
    
    private GameObject _hero = null;

    private void Start() {
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject == _hero) Destroy(gameObject);
    }
}
