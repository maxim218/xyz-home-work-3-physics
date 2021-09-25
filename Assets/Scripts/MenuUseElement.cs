using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUseElement : MonoBehaviour {
    private MenuUiGameControl _menuUiGameControl = null;

    private void Start() {
        Type type = typeof(MenuUiGameControl);
        _menuUiGameControl = (MenuUiGameControl) FindObjectOfType(type);
    }

    private bool _allow = true;
    
    public void UseItem() {
        if (!_allow) return;
        _allow = false;
        CurrentItemUse();
        StartCoroutine(AsyncAllowUsingAfterTime());
    }

    private IEnumerator AsyncAllowUsingAfterTime() {
        const float waitValue = 0.25f;
        yield return new WaitForSeconds(waitValue);
        _allow = true;
    }
    
    private void CurrentItemUse() {
        int index = _menuUiGameControl.GetIndexSelect();
        string message = "Use Item - " + index;
        Debug.Log(message);
        _menuUiGameControl.UseElement(index);
    }
}
