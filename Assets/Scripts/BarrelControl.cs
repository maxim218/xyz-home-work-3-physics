using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BarrelControl : MonoBehaviour {
    [Header("Money Prefabs")] 
    [SerializeField] private GameObject moneyA = null;
    [SerializeField] private GameObject moneyB = null;

    [Header("Probability number")] 
    [SerializeField] private int probabilityValue = 50;
    
    private bool MoneyTypeGet() {
        float value = Random.Range(15, 85);
        bool condition = (value < probabilityValue);
        return condition;
    }
    
    private void CreateMoney(Vector3 position) {
        if (MoneyTypeGet() == true) {
            GameObject objectMoney = Instantiate(moneyA) as GameObject;
            objectMoney.transform.position = position;
            objectMoney.transform.Translate(Vector3.up * 0.75f);
        } else {
            GameObject objectMoney = Instantiate(moneyB) as GameObject;
            objectMoney.transform.position = position;
            objectMoney.transform.Translate(Vector3.up * 0.75f);
        }
    }

    [Header("Barrel sprites")]
    [SerializeField] private Sprite sprite3 = null;
    [SerializeField] private Sprite sprite2 = null;
    [SerializeField] private Sprite sprite1 = null;

    private SpriteRenderer _spriteRenderer = null;
    private int _countValue = 3;

    [Header("Prefab broken barrel")] 
    [SerializeField] private GameObject brokenBarrelPrefab = null;
    
    private void Start() {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetSprite(_countValue);
    }

    [ContextMenu("Barrel Damage Method")]
    public void BarrelDamage() {
        _countValue -= 1;
        
        if (_countValue > 0) {
            SetSprite(_countValue);
        } else {
            // create broken part and set position
            GameObject brokenObj = Instantiate(brokenBarrelPrefab) as GameObject;
            brokenObj.transform.position = transform.position;
            brokenObj.transform.Translate(0, -0.25f, 0, Space.World);
            // money create
            CreateMoney(transform.position);
            // drop barrel
            Destroy(gameObject);
        }
    }
    
    private void SetSprite(int value) {
        switch (value) {
            case 3: _spriteRenderer.sprite = sprite3; break;
            case 2: _spriteRenderer.sprite = sprite2; break;
            case 1: _spriteRenderer.sprite = sprite1; break;
        }
    }
}
