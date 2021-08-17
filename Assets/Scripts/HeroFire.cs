using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFire : MonoBehaviour {
    [SerializeField] private int countKnife = 0;

    public void SetCountKnife(int value) {
        countKnife = value;
    }
    
    private bool _allow = true;
    
    public void Fire() {
        if (countKnife <= 1) return;
        if (!_allow) return;
        _allow = false;
        FireKnife();
        countKnife--;
        StartCoroutine( WaitAndAllow() );
    }

    private IEnumerator WaitAndAllow() {
        const float waitTime = 0.25f;
        yield return new WaitForSeconds(waitTime);
        _allow = true;
    }

    [SerializeField] private GameObject flySwordPrefab = null;
    
    private void FireKnife() {
        // message
        const string msg = "Hero Fire";
        Debug.Log(msg);
        
        // create knife
        GameObject sword = Instantiate(flySwordPrefab) as GameObject;
        
        // direction control
        GameObject hero = GameObject.Find("Hero");
        bool flipX = hero.GetComponent<SpriteRenderer>().flipX;
        sword.GetComponent<SpriteRenderer>().flipX = flipX;
        int direction = flipX ? -1 : 1;
        sword.GetComponent<FlySwordControl>().SetDirection(direction);
        
        // start pos
        const float deltaModule = 0.9f;
        float deltaHorizontal = deltaModule * direction;
        SwordPositionSet(sword, deltaHorizontal + transform.position.x, transform.position.y);
    }

    private static void SwordPositionSet(GameObject sword, float x, float y) {
        const float z = 5;
        sword.transform.position = new Vector3(x, y, z);
    }
}
