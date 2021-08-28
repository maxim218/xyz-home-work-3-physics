using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOfGunControl : MonoBehaviour {
    private Dictionary <string, Component> _dictionaryComponents = null;

    private GameObject _hero = null;
    
    private void Start() {
        // find hero
        _hero = GameObject.Find("Hero");
        // init dictionary
        _dictionaryComponents = ComponentGetter.GetDictionaryComponents(gameObject);
    }

    [SerializeField] private float speedX = 0;
    [SerializeField] private float speedY = 0;
    [SerializeField] private float counterDelta = 0;

    private float _counterMoving = 0;
    
    private void FixedUpdate() {
        // component
        Rigidbody2D compRigidbody2D = (Rigidbody2D) _dictionaryComponents["Rigidbody2D"];
        
        // change speed
        _counterMoving += counterDelta;
        float verticalSpeed = Mathf.Cos(_counterMoving) * speedY;
        compRigidbody2D.velocity = new Vector2(speedX, verticalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == _hero) _hero.GetComponent<ControlHealth>().AddLives(-1);
        Destroy(gameObject);
    }
}
