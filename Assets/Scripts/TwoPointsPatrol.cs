using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointsPatrol : MonoBehaviour {
    private Vector3 _pointA = Vector3.zero;
    private Vector3 _pointB = Vector3.zero;
    private bool _flagTarget = true;

    protected void SetPoints(Vector3 firstPoint, Vector3 secondPoint) {
        _pointA = firstPoint;
        _pointB = secondPoint;
    }

    protected void MoveToGoal(float speed) {
        Vector3 current = transform.position;
        Vector3 target = _flagTarget ? _pointA : _pointB;
        transform.position = Vector3.MoveTowards(current, target, speed * Time.deltaTime);
    }

    protected void ControlChangingGoal() {
        Vector3 current = transform.position;
        Vector3 target = _flagTarget ? _pointA : _pointB;
        float distance = Vector2.Distance((Vector2) current, (Vector2) target);
        if (distance < 0.1f) _flagTarget = !_flagTarget;
    }

    protected bool GetTargetFlag() {
        return _flagTarget;
    }
}
