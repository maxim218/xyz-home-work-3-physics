using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable] public struct MenuElement {
    public GameObject obj;
    public int currentValue;
    
    private Image image;
    private Text text;

    public UnityEvent useThisElementEvent;

    public void InitImage() {
        image = obj.GetComponent<Image>();
    }

    public Image ThisImage() {
        return image;
    }

    public void InitText() {
        GameObject child = obj.transform.Find("Text").gameObject;
        text = child.GetComponent<Text>();
    }

    public void SetTextValue() {
        text.text = "" + currentValue;
    }
}

public class MenuUiGameControl : MonoBehaviour {
    public void UseElement(int index) {
        if (_arrayElements[index].currentValue > 0) {
            _arrayElements[index].currentValue -= 1;
            RenderTextValues();
            _arrayElements[index].useThisElementEvent.Invoke();
        } else {
            const string msg = "Source is empty";
            Debug.Log(msg);
        }
    }
    
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.white;

    [SerializeField] private MenuElement [] _arrayElements = null;

    private void InitArrayComponents() {
        for (int i = 0; i < _arrayElements.Length; i++) {
            int index = i;
            _arrayElements[index].InitImage();
            _arrayElements[index].InitText();
        }
    }

    private void RenderTextValues() {
        for (int i = 0; i < _arrayElements.Length; i++) {
            int index = i;
            _arrayElements[index].SetTextValue();
        }
    }
    
    private void MarkSelectedElement(int index) {
        for (int i = 0; i < _arrayElements.Length; i++) _arrayElements[i].ThisImage().color = normalColor;
        _arrayElements[index].ThisImage().color = selectedColor;
    }

    [SerializeField] private int indexSelect = 0;
    
    private void Start() {
        InitArrayComponents();
        RenderTextValues();
        MarkSelectedElement(indexSelect);
    }

    private bool _allow = true;
    
    public void NextElement() {
        if (!_allow) return;
        _allow = false;
        ChangeCurrentElement();
        StartCoroutine(AsyncWaitAndAllow());
    }

    private IEnumerator AsyncWaitAndAllow() {
        const float wait = 0.25f;
        yield return new WaitForSeconds(wait);
        _allow = true;
    }
    
    private void ChangeCurrentElement() {
        const string msg = "Next Element";
        Debug.Log(msg);
        indexSelect++;
        indexSelect %= _arrayElements.Length;
        MarkSelectedElement(indexSelect);
    }

    public int GetIndexSelect() {
        return indexSelect;
    }
}
