using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLive : MonoBehaviour {
    private void OnDestroy() {
        PlayerMoving playerMoving = _hero.GetComponent<PlayerMoving>();
        playerMoving.AddThreeLives();
    }
    
    private GameObject _hero = null;

    private void Start() {
        _hero = GameObject.Find("Hero");
    }

    private void Update() {
        Vector2 positionHero = ToggleElementControl.GetPositionXY(_hero);
        Vector2 positionFirstAidKit = ToggleElementControl.GetPositionXY(gameObject);
        float distance = Vector2.Distance(positionHero, positionFirstAidKit);
        if (distance < 0.9f) Destroy(gameObject);
    }
}
