using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour {
    private GameObject _hero = null;

    private void Start() {
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        _hero = heroControl.gameObject;
    }

    [SerializeField] private GameObject btnForAttack = null;
    
    private void ButtonAttackActivate()
    {
        if (btnForAttack != null)
            btnForAttack.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject != _hero) return;
        HeroKnifeControl script = _hero.GetComponent<HeroKnifeControl>();
        script.CatchKnife();
        ButtonAttackActivate();
        SetHeroKnifeCount(_hero);
        Destroy(gameObject);
    }

    private static void SetHeroKnifeCount(GameObject hero) {
        const int knifeCount = 15;
        HeroFire scr = hero.GetComponent<HeroFire>();
        if (scr) scr.SetCountKnife(knifeCount);
    }
}
