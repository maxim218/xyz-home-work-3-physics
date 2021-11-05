using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroControl : MonoBehaviour {
    private int _sumValue = 0;

    private void Start() {
        GameObject saveObj = GameObject.Find("--X--X--SessionStore--X--X--");
        try
        {
            SessionStoreControl script = saveObj.GetComponent<SessionStoreControl>();
            _sumValue = script.MoneyStore;
        }
        catch
        {
            const string warningMessage = "Session Store Control was not found";
            Debug.LogWarning(warningMessage);
        }
    }

    public int GetSumValue() {
        return _sumValue;
    }
    
    public void NewLevelLoad() {
        StartCoroutine(LoadSceneAsync());
    }
    
    private static IEnumerator LoadSceneAsync() {
        const string sceneNameString = "SampleScene";
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameString);
        while(!operation.isDone) yield return new WaitForSeconds(1);
    }

    public void MoneyAdd(int moneyValue) {
        string message = "Money: " + moneyValue;
        Debug.Log(message);
        _sumValue += moneyValue;
    }
}
