using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyControl : MonoBehaviour {
    [SerializeField] private UnityEvent catchMoneyAction;

    private void OnTriggerEnter2D(Collider2D other) {
        HeroControl heroControl = other.GetComponent<HeroControl>();
        if (!heroControl) return;
        catchMoneyAction.Invoke();
        Destroy(gameObject);
    }
}
