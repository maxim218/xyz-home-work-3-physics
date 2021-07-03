using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour {
    private GameObject _hero = null;
    
    private void Start() {
        // get hero
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
        // wait and destroy
        StartCoroutine( KillStar() );
    }

    private IEnumerator KillStar() {
        const float waitValue = 2.5f;
        yield return new WaitForSeconds(waitValue);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != _hero) return;
        ControlHealth controlHealth = _hero.GetComponent<ControlHealth>();
        controlHealth.AddLives(-1);
        Destroy(gameObject);
    }
}
