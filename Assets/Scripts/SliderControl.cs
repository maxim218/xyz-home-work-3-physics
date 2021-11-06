using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour {
    [SerializeField] private string prefixString = string.Empty;

    public void SetPrefixString(string prefixValue)
    {
        prefixString = prefixValue;
    }
    
    [SerializeField] private GameObject renderTextObj = null;

    private Text _textComp = null;
    
    private Slider _slider = null;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _textComp = renderTextObj.GetComponent<Text>();
    }

    private void FixedUpdate() {
        string content = prefixString + _slider.value;
        bool condition = (_textComp.text == content);
        if (!condition) {
            _textComp.text = content;
        }
    }

    public int GetSliderVal() {
        if (!_slider) return 0;
        int value = int.Parse(_slider.value + "");
        return value;
    }

    public void SetSliderVal(int value) {
        if (!_slider) return;
        _slider.value = value;
    }

    [SerializeField] private string sliderType = string.Empty;

    public string GetSliderType() {
        return sliderType;
    }
}
