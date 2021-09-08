using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossControl : MonoBehaviour {
    [SerializeField] private GameObject targetPistol = null;

    private void LateUpdate() {
        if (!targetPistol) return;
        Vector3 help = Vector3.right * 2f;
        transform.position = targetPistol.transform.TransformPoint(help);
    }

    private GameObject _hero = null;

    private void Start() {
        _hero = GameObject.Find("Hero");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject != _hero) return;
        ControlHealth script = _hero.GetComponent<ControlHealth>();
        if (script) script.ZeroHealth();
    }
}
