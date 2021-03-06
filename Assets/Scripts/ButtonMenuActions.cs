using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuActions : MonoBehaviour {
    [SerializeField] private GameObject menuMain = null;
    [SerializeField] private GameObject menuOptions = null;
    [SerializeField] private GameObject wndLoading = null;
    
    private void HideWindows()
    {
        menuMain.SetActive(false);
        menuOptions.SetActive(false);
    }

    private void ShowLoadingWindow()
    {
        wndLoading.SetActive(true);
    }
    
    public void OperationStart() {
        ClickSound();
        const string msg = "Operation - Start";
        Debug.Log(msg);
        HideWindows();
        ShowLoadingWindow();
        StartCoroutine(LoadSceneAsync());
    }

    public void OperationOptions() {
        ClickSound();
        const string msg = "Operation - Options";
        Debug.Log(msg);
        const bool flag = false;
        ChangeWindows(flag);
    }

    public void OperationBackToMainMenu() {
        ClickSound();
        LocalStorageControl.GetScriptStorage().SaveToDisk();
        const string msg = "Operation - Back To Main Menu";
        Debug.Log(msg);
        const bool flag = true;
        ChangeWindows(flag);
    }
    
    public void OperationExit() {
        ClickSound();
        const string msg = "Operation - Exit";
        Debug.Log(msg);
        Application.Quit();
    }

    private static IEnumerator LoadSceneAsync()
    {
        const float loadingWaitTimeDemo = 2.5f;
        yield return new WaitForSeconds(loadingWaitTimeDemo);
        
        const string nameScene = "LevelA";
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

    private static void ClickSound() {
        Type type = typeof(SoundControl);
        if (!(FindObjectsOfType(type) is SoundControl[] arr)) return;
        foreach (SoundControl soundControl in arr) 
            if ("SFX" == soundControl.TypeGet()) 
                soundControl.SoundPlayOnce();
    }
}
