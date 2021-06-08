using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealthControl : MonoBehaviour {
    public int GetLiveInteger() {
        return _heroLiveInt;
    }
    
    private int _heroLiveInt = 5;
    
    public void SubLive() {
        _heroLiveInt -= 1; 
        Debug.Log("Live: " + _heroLiveInt);
    }
    
    public void AddThreeLives() {
        _heroLiveInt += 3;
        Debug.Log("Live: " + _heroLiveInt);
    }
}
