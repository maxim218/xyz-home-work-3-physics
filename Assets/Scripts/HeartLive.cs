using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLive : MonoBehaviour {
    [SerializeField] private int liveDelta = 3;
    
    private GameObject _hero = null;

    private void Start() {
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject != _hero) return;
        ControlHealth controlHealth = _hero.GetComponent<ControlHealth>();
        controlHealth.AddLives(liveDelta);
        Destroy(gameObject);
    }
}
