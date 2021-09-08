using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBossControl : MonoBehaviour {
    private BossControl _bossControl = null;
    private LineRenderer _lineRenderer = null;
    
    [SerializeField] private Color _color = Color.black;
    [SerializeField] private float _width = 0;
    
    private void Start() {
        // get boss script
        Type type = typeof(BossControl);
        _bossControl = (BossControl) FindObjectOfType(type);
        
        // get renderer
        _lineRenderer = GetComponent<LineRenderer>();
        
        // line has 2 positions
        _lineRenderer.positionCount = 2;
        // line color
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        // line width
        _lineRenderer.startWidth = _width;
        _lineRenderer.endWidth = _width;
        // world position
        _lineRenderer.useWorldSpace = true;
    }

    public void SetColor(Color colorValue) {
        // line color
        _lineRenderer.startColor = colorValue;
        _lineRenderer.endColor = colorValue;
    }
    
    [SerializeField] private string elementKeyBoss = string.Empty;
    
    private void LateUpdate() {
        transform.position = _bossControl.gameObject.transform.position;
        
        Vector3 positionElement = _bossControl.GetPosition(elementKeyBoss);
        Vector3 positionCenter = (Vector3) _bossControl.GetCenterPosition();

        const float z = 9;
        positionElement.z = z;
        positionCenter.z = z;

        const int indexZero = 0;
        const int indexFirst = 1;
        _lineRenderer.SetPosition(indexZero, positionElement);
        _lineRenderer.SetPosition(indexFirst, positionCenter);
    }
}
