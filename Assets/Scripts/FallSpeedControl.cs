using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpeedControl : MonoBehaviour {
    private float _prevSpeedC = 0f;
    private float _prevSpeedB = 0f; 
    private float _prevSpeedA = 0f;

    private void RewriteSpeedValues(float speed) {
        _prevSpeedC = _prevSpeedB;
        _prevSpeedB = _prevSpeedA;
        _prevSpeedA = speed;
    }

    private Rigidbody2D _rigidbody2D = null;
    
    private void Start() {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
        float speed = _rigidbody2D.velocity.y;
        RewriteSpeedValues(speed);
    }

    public bool WasSpeedBig(float bigBorder) {
        float middle = (_prevSpeedC + _prevSpeedB + _prevSpeedA) / 3f;
        bool condition = (middle < bigBorder);
        return condition;
    }
}
