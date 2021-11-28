using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateControl : MonoBehaviour {
    public void InitGranate(GameObject hero, SpriteRenderer spriteRenderer) {
        // position control
        Vector3 positionHero = hero.transform.position;
        const float z = -12;
        transform.position = new Vector3(positionHero.x, positionHero.y, z);
        
        // force
        Rigidbody2D rigidbodyScript = GetComponent<Rigidbody2D>();
        Vector2 forceVector = ForceVectorGet(spriteRenderer);
        rigidbodyScript.AddForce(forceVector);
    }
    
    private static Vector2 ForceVectorGet(SpriteRenderer spriteRenderer) {
        float x = spriteRenderer.flipX ? -170 : 170;
        const float y = 130;
        return new Vector2(x, y);
    }

    private enum GoalZone
    {
        Top,
        Bottom
    }
    
    private bool _prohibitUpdate = true;
    private Vector3 _bottomPos = Vector3.zero;
    private Vector3 _topPos = Vector3.zero;
    private GoalZone _goalZone = GoalZone.Top;
    private const float Speed = 3.0f;
    private const float Distance = 0.25f;
    private bool _isAlreadyFlying = false;
    
    public void BeginFlying()
    {
        if (_isAlreadyFlying)
        {
            const string msg = "Already flying";
            Debug.Log(msg);
            return;
        }

        _isAlreadyFlying = true;

        // destroy rigidbody
        Rigidbody2D rigidbodyScript = GetComponent<Rigidbody2D>();
        if (rigidbodyScript)
            Destroy(rigidbodyScript);
        
        // current pos
        float x = transform.position.x;
        float y = transform.position.y;
        const float z = 12;
        
        // bottom pos
        float yBottom = y + 1;
        _bottomPos = new Vector3(x, yBottom, z);
        
        // top pos
        float yTop = y + 3;
        _topPos = new Vector3(x, yTop, z);
        
        // set goal zone
        _goalZone = GoalZone.Top;
        
        // allow updating
        _prohibitUpdate = false;
    }

    private void Update()
    {
        if (_prohibitUpdate)
            return;

        Vector3 currentPos = transform.position;
        Vector3 target = Vector3.zero;
        
        switch (_goalZone)
        {
            case GoalZone.Top:
                target = _topPos;
                transform.position = Vector3.MoveTowards(currentPos, target, Speed * Time.deltaTime);
                break;
            case GoalZone.Bottom:
                target = _bottomPos;
                transform.position = Vector3.MoveTowards(currentPos, target, Speed * Time.deltaTime);
                break;
        }

        float distance = Vector2.Distance((Vector2) transform.position, (Vector2) target);
        if (distance < Distance)
            ChangeGoal();
    }

    private void ChangeGoal()
    {
        bool condition = (GoalZone.Top == _goalZone);
        _goalZone = condition ? GoalZone.Bottom : GoalZone.Top;
    }
}
