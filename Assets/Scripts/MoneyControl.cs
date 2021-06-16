using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyControl : MonoBehaviour {
    [SerializeField] private int costs = 0;
    
    private void OnTriggerEnter2D(Collider2D other) {
        HeroControl heroControl = other.GetComponent<HeroControl>();
        if (!heroControl) return;
        heroControl.MoneyAdd(costs);
        Destroy(gameObject);
    }
}
