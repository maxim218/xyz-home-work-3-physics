using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMonstrControl : TwoPointsPatrol {
    [SerializeField] private float radiusDistance = 0f;

    private Vector3 _leftBorder = Vector3.zero;
    private Vector3 _rightBorder = Vector3.zero;

    private SpriteRenderer _spriteRenderer = null;

    private GameObject _hero = null;
    
    [SerializeField] private float speedMove = 1.5f;
    
    private void Start() {
        // hero
        _hero = GameObject.Find("Hero");
        // sprite renderer
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // get two positions
        Vector3 position = gameObject.transform.position;
        _leftBorder = new Vector3(position.x - radiusDistance, position.y, position.z);
        _rightBorder = new Vector3(position.x + radiusDistance, position.y, position.z);
        SetPoints(_leftBorder, _rightBorder);
    }

    private void Update() {
        if (SeeHeroCondition() == false) {
            SimplePatrol();
        } else {
            TryKillHero();
        }
    }

    private void SimplePatrol() {
        MoveToGoal(speedMove);
        ControlChangingGoal();
        _spriteRenderer.flipX = !GetTargetFlag();
    }

    private static float ModuleDelta(float a, float b) {
        float module = Mathf.Abs(a - b);
        return module;
    }
    
    private void TryKillHero() {
        // positions
        Vector3 heroPos = _hero.transform.position;
        Vector3 creaturePos = gameObject.transform.position;
        // moving
        if (ModuleDelta(heroPos.x, creaturePos.x) > 0.2f) {
            if (heroPos.x > creaturePos.x) {
                _spriteRenderer.flipX = true;
                transform.Translate(speedMove * Time.deltaTime, 0, 0);
            } else {
                _spriteRenderer.flipX = false;
                transform.Translate(-1 * speedMove * Time.deltaTime, 0, 0);
            }
        }
        // control out of position range
        if (_leftBorder.x > creaturePos.x) transform.position = _leftBorder;
        if (_rightBorder.x < creaturePos.x) transform.position = _rightBorder;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        const float radius = 0.1f;
        Gizmos.DrawSphere(_leftBorder, radius);
        Gizmos.DrawSphere(_rightBorder, radius);
    }

    private bool SeeHeroCondition() {
        Vector3 heroPos = _hero.transform.position;
        Vector3 creaturePos = gameObject.transform.position;
        float distance = Vector2.Distance((Vector2) heroPos, (Vector2) creaturePos);
        bool condition = (distance < 2.5f);
        return condition;
    }

    [SerializeField] private GameObject deadRobotPrefab = null;

    [ContextMenu("Kill Robot Method")]
    public void KillRobotMethod() {
        GameObject body = Instantiate(deadRobotPrefab) as GameObject;
        body.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == _hero) KillRobotMethod();
    }
}
