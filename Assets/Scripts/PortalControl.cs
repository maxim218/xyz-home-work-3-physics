using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour {
    private GameObject _hero = null;

    private void Start() {
        _hero = GameObject.Find("Hero");
    }

    [SerializeField] private GameObject exitObject = null;

    private void Update() {
        Vector2 posA = ToggleElementControl.GetPositionXY(_hero);
        Vector2 posB = ToggleElementControl.GetPositionXY(gameObject);

        float distance = Vector2.Distance(posA, posB);
        if (distance < 0.75f) MoveToExit();
    }

    private void MoveToExit() {
        Vector3 exitPos = exitObject.transform.position;
        SetPosition(exitPos.x, exitPos.y);
    }

    private void SetPosition(float x, float y) {
        _hero.transform.position = new Vector3(x, y, _hero.transform.position.z);
    }
}
