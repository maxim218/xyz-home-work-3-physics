using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHealth : MonoBehaviour {
    [SerializeField] private int lives = 5;
    
    public int Lives => lives;
    
    private void Start() {
        GameObject saveObj = GameObject.Find("--X--X--SessionStore--X--X--");
        try
        {
            SessionStoreControl script = saveObj.GetComponent<SessionStoreControl>();
            lives = script.HealthStore;
        }
        catch
        {
            const string warningMessage = "Session Store Control was not found";
            Debug.LogWarning(warningMessage);
        }
    }

    public void AddLives(int value) {
        lives += value;
        string message = "Lives: " + lives;
        Debug.Log(message);
    }

    public void ZeroHealth() {
        lives = 0;
    }
}