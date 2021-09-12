﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuActions : MonoBehaviour {
    [SerializeField] private GameObject menuMain = null;
    [SerializeField] private GameObject menuOptions = null;
    
    public void OperationStart() {
        const string msg = "Operation - Start";
        Debug.Log(msg);
        StartCoroutine(LoadSceneAsync());
    }

    public void OperationOptions() {
        const string msg = "Operation - Options";
        Debug.Log(msg);
        const bool flag = false;
        ChangeWindows(flag);
    }

    public void OperationBackToMainMenu() {
        const string msg = "Operation - Back To Main Menu";
        Debug.Log(msg);
        const bool flag = true;
        ChangeWindows(flag);
    }
    
    public void OperationExit() {
        const string msg = "Operation - Exit";
        Debug.Log(msg);
    }

    private static IEnumerator LoadSceneAsync() {
        const string nameScene = "BossScene";
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        while(!operation.isDone) yield return new WaitForSeconds(1);
    }

    private void ChangeWindows(bool flag) {
        ButtonsColorSimple();
        menuMain.SetActive(flag);
        menuOptions.SetActive(!flag);
        ButtonsColorSimple();
    }

    private static void ButtonsColorSimple() {
        Type type = typeof(ButtonMenuControl);
        if (!(FindObjectsOfType(type) is ButtonMenuControl[] arr)) return;
        if (arr.Length == 0) return;
        foreach (ButtonMenuControl script in arr) script.ColorSimple();
    }
    
    private void Start() {
        const bool flag = true;
        ChangeWindows(flag);
    }
}
