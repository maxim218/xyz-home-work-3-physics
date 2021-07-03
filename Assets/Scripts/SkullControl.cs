using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullControl : MonoBehaviour {
    private GameObject _hero = null;

    [SerializeField] private GameObject [] arr = null;

    [SerializeField] private float speedMove = 0;
    
    private void Start() {
        // get hero
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != _hero) return;
        ControlHealth controlHealth = _hero.GetComponent<ControlHealth>();
        controlHealth.AddLives(-1);
        Destroy(gameObject);
    }

    private int _indexTarget = 0;
    
    private void Update() {
        // control empty
        if (arr.Length == 0) return;
        
        // move to goal
        GameObject targetObject = arr[_indexTarget];
        Vector3 targetPosition = targetObject.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);

        // change goal
        float distance = Vector2.Distance((Vector2) transform.position, (Vector2) targetPosition);
        if (distance < 0.15f) _indexTarget++;
        _indexTarget %= arr.Length;
    }
}
