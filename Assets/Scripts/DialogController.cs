using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private Dictionary<string, string> _dictionaryFiles = null;

    [SerializeField] private string[] dialogNamesArr = null;

    [SerializeField] private Text informationTextComponent = null;

    [SerializeField] private GameObject contentWindow = null;

    [SerializeField] private Animator animator = null;
    
    private void InitDialogDictionary()
    {
        _dictionaryFiles = new Dictionary <string, string> ();
        foreach (string nameOfDialog in dialogNamesArr)
        {
            TextAsset asset = Resources.Load<TextAsset>(nameOfDialog);
            _dictionaryFiles[nameOfDialog] = asset.text.Trim();
        }
    }
    
    private void Start()
    {
        InitDialogDictionary();
        HideWindow();
    }

    [ContextMenu("Hide Window With Dialog")]
    private void HideWindow()
    {
        ResetFields();
        const bool activeFlag = false;
        contentWindow.SetActive(activeFlag);
    }

    [ContextMenu("Show Window With Dialog")]
    private void ShowWindow()
    {
        ResetFields();
        const bool activeFlag = true;
        contentWindow.SetActive(activeFlag);
        animator.CrossFade("CanvasAnimation", 0);
    }
    
    public void CloseBtnClick()
    {
        const string msg = "Close Btn Click";
        Debug.Log(msg);
        HideWindow();
        // reset data and stop animation
        ResetFields();
        AnimationStop();
        ResetFields();
    }

    public void NextBtnClick()
    {
        const string msg = "Next Btn Click";
        Debug.Log(msg);
        // next string animation
        _stringInFileNum += 1;
        string message = GetStringFromText(_dictionaryFiles[currentDialog], _stringInFileNum);
        if (string.IsNullOrEmpty(message))
            CloseBtnClick();
        else
            AnimationStart(message);
    }

    private IEnumerator _coroutine = null;
    private int _charNumber = 0;
    private string _animationString = "";

    private int _stringInFileNum = 0;
    
    private void AnimationStop()
    {
        try {
            StopCoroutine(_coroutine);
        } catch {
            /* empty */
        }
    }

    private void ResetFields()
    {
        informationTextComponent.text = "";
        _charNumber = 0;
        _animationString = "";
    }
    
    private void AnimationStart(string message)
    {
        // reset data and stop animation
        ResetFields();
        AnimationStop();
        ResetFields();

        // start new animation
        _animationString = message;
        _coroutine = AsyncAnimationOfText();
        StartCoroutine(_coroutine);
    }

    private IEnumerator AsyncAnimationOfText()
    {
        const float windowAnimationWaitTime = 1f;
        yield return new WaitForSeconds(windowAnimationWaitTime);
        
        while (true) {
            const float waitTime = 0.1f;
            yield return new WaitForSeconds(waitTime);
            
            if (_charNumber < _animationString.Length) {
                informationTextComponent.text += _animationString[_charNumber];
                _charNumber += 1;
            } else {
                yield break;
            }
        }
    }

    [SerializeField] private string currentDialog = string.Empty;

    public void SetCurrentDialog(string key)
    {
        currentDialog = key;
    }
    
    [ContextMenu("Dialog Begin")]
    public void DialogBegin()
    {
        _stringInFileNum = 0;
        
        if (string.IsNullOrEmpty(currentDialog))
            return;

        ShowWindow();
        
        string message = GetStringFromText(_dictionaryFiles[currentDialog], _stringInFileNum);
        if (string.IsNullOrEmpty(message))
            CloseBtnClick();
        else
            AnimationStart(message);
    }

    private static string GetStringFromText(string text, int num)
    {
        try {
            const char separator = '\n';
            string row = text.Split(separator)[num].Trim();
            return row;
        } catch {
            return string.Empty;
        }
    }
}
