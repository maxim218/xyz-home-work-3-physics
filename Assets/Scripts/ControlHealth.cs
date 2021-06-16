using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHealth : MonoBehaviour {
    [SerializeField] private int lives = 5;
    
    public int Lives => lives;

    public void AddLives(int value) {
        lives += value;
        string message = "Lives: " + lives;
        Debug.Log(message);
    }
}