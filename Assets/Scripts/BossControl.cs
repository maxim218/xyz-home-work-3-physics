using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour {
    public Vector3 GetPosition(string elemKey) {
        switch (elemKey) {
            case "heart":
                return _heartObj.transform.position;
            case "pistolA":
                return _pistolObjA.transform.position;
            case "pistolB":
                return _pistolObjB.transform.position;
            case "pistolC":
                return _pistolObjC.transform.position;
            default:
                return Vector3.zero;
        }
    }
    
    private GameObject _heartObj = null;
    private GameObject _pistolObjA = null;
    private GameObject _pistolObjB = null;
    private GameObject _pistolObjC = null;

    private void InitChildrenObjects() {
        // get heart
        _heartObj = gameObject.transform.Find("boss_heart").gameObject;
        // get pistols
        _pistolObjA = gameObject.transform.Find("boss_pistol_A").gameObject;
        _pistolObjB = gameObject.transform.Find("boss_pistol_B").gameObject;
        _pistolObjC = gameObject.transform.Find("boss_pistol_C").gameObject;
    }

    private Vector3 _startPosHeart = Vector3.zero;
    private Vector3 _startPosA = Vector3.zero;
    private Vector3 _startPosB = Vector3.zero;
    private Vector3 _startPosC = Vector3.zero;

    private void InitStartPositions() {
        // position heart
        _startPosHeart = _heartObj.transform.position;
        // position pistols
        _startPosA = _pistolObjA.transform.position;
        _startPosB = _pistolObjB.transform.position;
        _startPosC = _pistolObjC.transform.position;
    }

    private float _centerX = 0;
    private float _centerY = 0;

    private void InitCenterPosition() {
        GameObject centerObject = gameObject.transform.Find("boss_center").gameObject;
        Vector3 position = centerObject.transform.position;
        _centerX = position.x;
        _centerY = position.y;
    }

    public Vector2 GetCenterPosition() {
        return new Vector2(_centerX, _centerY);
    }
    
    private void Start() {
        InitChildrenObjects();
        InitStartPositions();
        InitCenterPosition();
    }

    private static float GetRotateX(float angle, float xc, float yc, float x, float y) {
        float resX = (x - xc) * Mathf.Cos(angle) + (y - yc) * Mathf.Sin(angle) + xc;
        return resX;
    }
    
    private static float GetRotateY(float angle, float xc, float yc, float x, float y) {
        float resY = (y - yc) * Mathf.Cos(angle) - (x - xc) * Mathf.Sin(angle) + yc;
        return resY;
    }

    private static void RotateTarget(GameObject target, float radiansAngle, Vector3 startPosition, float centerX, float centerY) {
        float resX = GetRotateX(radiansAngle, centerX, centerY, startPosition.x, startPosition.y);
        float resY = GetRotateY(radiansAngle, centerX, centerY, startPosition.x, startPosition.y);
        Vector3 resultPosition = new Vector3 (resX, resY, target.transform.position.z);
        target.transform.position = resultPosition;
    }

    [SerializeField] private float deltaAngleRadians = 0;
    [SerializeField] private float currentAngleRadians = 0;
    
    private void Update() {
        if (_youAreDead) return;
        
        // change angle
        currentAngleRadians += deltaAngleRadians * Time.deltaTime;

        // move heart
        RotateTarget(_heartObj, currentAngleRadians, _startPosHeart, _centerX, _centerY);
        
        // move pistols
        RotateTarget(_pistolObjA, currentAngleRadians, _startPosA, _centerX, _centerY);
        RotateTarget(_pistolObjB, currentAngleRadians, _startPosB, _centerX, _centerY);
        RotateTarget(_pistolObjC, currentAngleRadians, _startPosC, _centerX, _centerY);
    }

    private static void WatchToCenter(float centerX, float centerY, GameObject target) {
        Vector2 targetPosition = (Vector2) target.transform.position;
        Vector2 centerPosition = new Vector2(centerX, centerY);
        Vector2 deltaPosition = targetPosition - centerPosition;
        target.transform.right = (Vector3) deltaPosition;
    }
    
    private void LateUpdate() {
        if (_youAreDead) return;
        
        // make pistols watch to the center
        WatchToCenter(_centerX, _centerY, _pistolObjA);
        WatchToCenter(_centerX, _centerY, _pistolObjB);
        WatchToCenter(_centerX, _centerY, _pistolObjC);
    }

    private bool _youAreDead = false;

    public void BossDie() {
        _youAreDead = true;
    }
}
