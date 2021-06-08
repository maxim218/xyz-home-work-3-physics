using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour {
    private GameObject _hero = null;

    private void Start() {
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }

    [SerializeField] private GameObject exitObject = null;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject == _hero) MoveToExit();
    }
    
    private void MoveToExit() {
        Vector3 exitPos = exitObject.transform.position;
        SetPosition(exitPos.x, exitPos.y);
    }

    private void SetPosition(float x, float y) {
        _hero.transform.position = new Vector3(x, y, _hero.transform.position.z);
    }
}
