using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFabric : MonoBehaviour {
    [SerializeField] private GameObject prefabDust = null;
    [SerializeField] private GameObject prefabJumpDust = null;
    [SerializeField] private GameObject prefabFallDust = null;

    [SerializeField] private bool circleFlag = true;
    
    private PlayerMoving _playerMoving = null;
    private FallSpeedControl _fallSpeedControl = null;

    private void Start() {
        _fallSpeedControl = gameObject.GetComponent<FallSpeedControl>();
        _playerMoving = gameObject.GetComponent<PlayerMoving>();
        StartCoroutine(TimeRepeatAction());
    }

    private void FixedUpdate() {
        // control fall with big speed and create dust
        const float bigSpeed = -4f;
        bool isSpeedBig = _fallSpeedControl.WasSpeedBig(bigSpeed);
        bool isStayOnGround = _playerMoving.IsHeroStayOnGround();
        if (isSpeedBig && isStayOnGround) {
            GameObject fallDust = Instantiate(prefabFallDust) as GameObject;
            SetDustPosition(fallDust);
            DustControl dustControl = fallDust.GetComponent<DustControl>();
            dustControl.DeleteDustAfterTime();
        }
        
        // control and generate vertical dust
        bool stayOnGround = _playerMoving.IsHeroStayOnGround();
        bool positiveVerticalSpeed = (_playerMoving.GetVelocityVertical() > 0.05f);
        if (!stayOnGround || !positiveVerticalSpeed) return;
        GameObject jumpDust = Instantiate(prefabJumpDust) as GameObject;
        SetDustPosition(jumpDust);
    }

    private IEnumerator TimeRepeatAction() {
        while (circleFlag) {
            // wait 
            const float waitTime = 0.1f;
            yield return new WaitForSeconds(waitTime);

            // control and generate horizontal dust
            bool groundFlag = _playerMoving.IsHeroStayOnGround();
            if (!groundFlag) continue;
            float vX = _playerMoving.GetVelocityHorizontal();
            const float rightDelta = 0.5f;
            const float leftDelta = -0.5f;
            if (vX > rightDelta) DustGenerate(true);
            if (vX < leftDelta) DustGenerate(false);
        }
    }

    private GameObject CreateDust() {
        GameObject dust = Instantiate(prefabDust) as GameObject;
        return dust;
    }

    private void SetDustPosition(GameObject dust) {
        const float z = -1.5f;
        Vector3 position = gameObject.transform.position;
        dust.transform.position = new Vector3(position.x, position.y, z);
    }

    private static void SetDustDirection(GameObject dust, bool flipCondition) {
        SpriteRenderer spriteRenderer = dust.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = flipCondition;
    }

    private void DustGenerate(bool flipCondition) {
        GameObject dust = CreateDust();
        SetDustPosition(dust);
        SetDustDirection(dust, flipCondition);
    }
}
