using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateControl : MonoBehaviour {
    public void InitGranate(GameObject hero, SpriteRenderer spriteRenderer) {
        // position control
        Vector3 positionHero = hero.transform.position;
        const float z = -12;
        transform.position = new Vector3(positionHero.x, positionHero.y, z);
        
        // force
        Rigidbody2D rigidbodyScript = GetComponent<Rigidbody2D>();
        Vector2 forceVector = ForceVectorGet(spriteRenderer);
        rigidbodyScript.AddForce(forceVector);
    }
    
    private static Vector2 ForceVectorGet(SpriteRenderer spriteRenderer) {
        float x = spriteRenderer.flipX ? -170 : 170;
        const float y = 130;
        return new Vector2(x, y);
    }
}
