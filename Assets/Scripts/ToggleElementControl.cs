using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleElementControl : MonoBehaviour {
    private GameObject _hero = null;
    private Animator _animator = null;
    
    private void Start() {
        _hero = GameObject.Find("Hero");
        _animator = gameObject.GetComponent<Animator>();
    }

    public static Vector2 GetPositionXY(GameObject obj) {
        Vector3 pos = obj.transform.position;
        return new Vector2(pos.x, pos.y);
    }

    private bool _shouldUpdate = true;
    
    private void Update() {
        if (!_shouldUpdate) return;

        Vector2 togglePos = GetPositionXY(gameObject);
        Vector2 heroPos = GetPositionXY(_hero);

        float distance = Vector2.Distance(togglePos, heroPos);
        
        if (!(distance < 0.75f)) return;
        _shouldUpdate = false;
        _animator.CrossFade("ToggleElementAnimation", 0);
    }

    [SerializeField] private GameObject door = null;
    
    public void FinishToggleAnimation() {
        Animator animatorDoor = door.GetComponent<Animator>();
        animatorDoor.CrossFade("DoorAnimation", 0);
    }
}
