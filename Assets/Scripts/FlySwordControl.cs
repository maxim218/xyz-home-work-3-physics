using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwordControl : MonoBehaviour {
    [SerializeField] private float speed = 0;
    
    private int _direction = 0;

    public void SetDirection(int value) {
        _direction = value;
    }
    
    private bool _canMove = true;

    private Rigidbody2D _rigidbody2D = null;
    
    private void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (_canMove) {
            float x = speed * _direction;
            const float y = 0;
            _rigidbody2D.velocity = new Vector2(x, y);
        } else {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    [SerializeField] private GameObject prefabSwordInWall = null;

    private void CreateSwordInWall(Vector3 position) {
        GameObject swordInWall = Instantiate(prefabSwordInWall) as GameObject;
        swordInWall.transform.position = position;
        bool flipX = GetComponent<SpriteRenderer>().flipX;
        swordInWall.GetComponent<SpriteRenderer>().flipX = flipX;
        float horizontalDelta = flipX ? -0.3f : 0.3f;
        swordInWall.transform.Translate(horizontalDelta, 0, 0);
    }

    private static void IsBarrelControl(GameObject hitObj) {
        BarrelControl script = hitObj.GetComponent<BarrelControl>();
        if (script) script.BarrelDamage();
    }

    private static void IsMonstrControl(GameObject hitObj) {
        MovingMonstrControl script = hitObj.GetComponent<MovingMonstrControl>();
        if (script) script.KillRobotMethod();
    }

    private static void IsGunControl(GameObject hitObj) {
        GunControl script = hitObj.GetComponent<GunControl>();
        if (script) script.GunKill();
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        // control having Ground
        GameObject hitObj = collision.gameObject;
        Ground script = hitObj.GetComponent<Ground>();
        if (!script) return;
        
        // prohibit move
        _canMove = false;
        
        // sword
        CreateSwordInWall(transform.position);
        
        // type of hit obj
        IsBarrelControl(hitObj);
        IsGunControl(hitObj);
        IsMonstrControl(hitObj);

        // destroy
        Destroy(gameObject);
    }
}
