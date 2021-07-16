using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour {
    private GameObject _hero = null;
    private SpriteRenderer _heroSpriteRenderer = null;
    private SpriteRenderer _knifeSpriteRenderer = null;

    private void Start() {
        // knife
        _knifeSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // hero
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
        _heroSpriteRenderer = _hero.GetComponent<SpriteRenderer>();
    }

    private void PositionSet() {
        // calculate delta horizontal
        bool condition = _heroSpriteRenderer.flipX;
        float delta = condition ? -0.7f : 0.7f;
        
        // calculate pos
        float x = _hero.transform.position.x + delta;
        float y = _hero.transform.position.y;
        const float z = -3.5f;
        
        // set pos
        transform.position = new Vector3(x, y, z);
    }

    private void RepeatingAction() {
        _knifeSpriteRenderer.flipX = _heroSpriteRenderer.flipX;
        PositionSet();
    }
    
    private void Update() {
        RepeatingAction();
    }

    private void FixedUpdate() {
        RepeatingAction();
    }

    [ContextMenu("Attack Sword Destroy")]
    public void AttackSwordDestroy() {
        _hero.GetComponent<HeroKnifeControl>().AllowCreatingYes();
        Destroy(gameObject);
    }

    public void TryHitElements() {
        // control existing
        if (!(FindObjectsOfType(typeof(BarrelControl)) is BarrelControl[] arr)) return;
        
        // visit all barrels
        foreach (BarrelControl barrelScript in arr) {
            if (!barrelScript) continue;
            Vector2 positionBarrel = (Vector2) barrelScript.gameObject.transform.position;
            Vector2 positionSword = (Vector2) transform.position;
            float distance = Vector2.Distance(positionBarrel, positionSword);
            if (distance < 1.1f) barrelScript.BarrelDamage();
        }
    }
}
