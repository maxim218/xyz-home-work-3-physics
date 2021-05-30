using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroControl : MonoBehaviour {
    private int _sumValue = 0;

    private bool _drawBtnFlag = true;
    
    private void OnGUI() {
        // label with score
        string content = "Count: " + _sumValue;
        GUI.Box(new Rect(140, 20, 100, 26), content);
        
        // reload scene btn
        if (!_drawBtnFlag) return;
        const string btnText = "Reload scene";
        if (!GUI.Button(new Rect(260, 20, 150, 26), btnText)) return;
        _drawBtnFlag = false;
        StartCoroutine(LoadSceneAsync());
    }

    private static IEnumerator LoadSceneAsync() {
        const string sceneNameString = "SampleScene";
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameString);
        while(!operation.isDone) yield return new WaitForSeconds(1);
    }

    private void MoneyAdd(int moneyValue) {
        string message = "Money: " + moneyValue;
        Debug.Log(message);
        _sumValue += moneyValue;
    }
    
    public void Catch_2() {
        const int costs = 2;
        MoneyAdd(costs);
    }
    
    public void Catch_4() {
        const int costs = 4;
        MoneyAdd(costs);
    }
}
