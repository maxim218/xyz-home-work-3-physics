using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSawControl : PosCopy {
    [SerializeField] private GameObject targetCreature = null;

    private GameObject _hero = null;
    
    private SpriteRenderer _creatureSpriteRenderer = null;
    private SpriteRenderer _mySpriteRenderer = null;
    
    private void Start() {
        _hero = GameObject.Find("Hero");
        _creatureSpriteRenderer = targetCreature.GetComponent<SpriteRenderer>();
        _mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        if (!targetCreature) {
            Destroy(gameObject);
            return;
        }
        
        Vector3 position = targetCreature.transform.position;
        float posX = _creatureSpriteRenderer.flipX ? (position.x + 0.5f) : (position.x - 0.5f);
        float posY = position.y - 0.05f;
        CopyPosition(posX, posY);

        _mySpriteRenderer.flipX = !_creatureSpriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // is hero
        if (collision.gameObject == _hero) _hero.GetComponent<ControlHealth>().ZeroHealth();
        
        // is knife
        FlySwordControl script = collision.gameObject.GetComponent<FlySwordControl>();
        if (!script) return;
        GameObject knifeDead = Instantiate(prefabDeadKnife) as GameObject;
        knifeDead.transform.position = collision.gameObject.transform.position;
        knifeDead.GetComponent<SpriteRenderer>().flipX = !_mySpriteRenderer.flipX;
        Destroy(collision.gameObject);
    }

    [SerializeField] private GameObject prefabDeadKnife = null;
}
