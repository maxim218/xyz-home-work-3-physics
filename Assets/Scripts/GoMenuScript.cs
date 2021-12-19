using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMenuScript : MonoBehaviour
{
    private static string SceneMenuName => "MainMenuScene";

    private bool _allowRun = true;

    private void GoMainMenu()
    {
        if(_allowRun)
        {
            _allowRun = false;
            StartCoroutine(LoadSceneAsync());
        }
    }

    private static IEnumerator LoadSceneAsync()
    {
        // wait
        const float waitValue = 3f;
        yield return new WaitForSeconds(waitValue);
        // load scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneMenuName);
        while (!operation.isDone) yield return new WaitForSeconds(1);
    }

    public static void MainMenuMove()
    {
        GoMenuScript script = FindObjectOfType<GoMenuScript>();
        if (script)
            script.GoMainMenu();
    }
}
