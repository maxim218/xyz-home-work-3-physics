using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionStoreControl : MonoBehaviour {
    private void Start() {
        // log message
        const string msg = "SessionStore - Start";
        Debug.Log(msg);
        
        // prohibit delete
        DontDestroyOnLoad(gameObject);
    }

    private static GameObject GetHeroObj() {
        HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
        GameObject hero = heroControl.gameObject;
        return hero;
    }
    
    private int _moneyStore = 0;
    private int _healthStore = 5;
    
    public int MoneyStore => _moneyStore; 
    public int HealthStore => _healthStore;

    private static int TakeMoney(GameObject hero) {
        HeroControl heroControl = hero.GetComponent<HeroControl>();
        int money = heroControl.GetSumValue();
        return money;
    }

    private static int TakeHealth(GameObject hero) {
        ControlHealth controlHealth = hero.GetComponent<ControlHealth>();
        int health = controlHealth.Lives;
        return health;
    }

    [ContextMenu("Set Store Values Method")]
    public void SetStoreValues() {
        // get hero
        GameObject hero = GetHeroObj();
        
        // get money and health
        _moneyStore = TakeMoney(hero);
        _healthStore = TakeHealth(hero);
        
        // log info
        string msg = "Saving --- " + "Money: " + _moneyStore + " --- " + "Health: " + _healthStore;
        Debug.Log(msg);
    }
}
