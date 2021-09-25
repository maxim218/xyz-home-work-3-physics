using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryActionsManager : MonoBehaviour {
    [SerializeField] private GameObject granateCirclePrefab = null;
    [SerializeField] private GameObject granateRectanglePrefab = null;

    private void Start() {
        Type type = typeof(PlayerMoving);
        PlayerMoving playerMoving = (PlayerMoving) FindObjectOfType(type);
        _hero = playerMoving.gameObject;
        _spriteRenderer = _hero.GetComponent<SpriteRenderer>();
        _controlHealth = _hero.GetComponent<ControlHealth>();
    }

    private GameObject _hero = null;
    private SpriteRenderer _spriteRenderer = null;
    private ControlHealth _controlHealth = null;
    
    public void CircleGranate() {
        GameObject granate = Instantiate(granateCirclePrefab) as GameObject;
        GranateControl granateControl = granate.GetComponent<GranateControl>();
        granateControl.InitGranate(_hero, _spriteRenderer);
    }

    public void RectangleGranate() {
        GameObject granate = Instantiate(granateRectanglePrefab) as GameObject;
        GranateControl granateControl = granate.GetComponent<GranateControl>();
        granateControl.InitGranate(_hero, _spriteRenderer);
    }

    public void HealPlayer(int value) {
        _controlHealth.AddLives(value);
    }
}
