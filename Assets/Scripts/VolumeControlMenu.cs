using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControlMenu : MonoBehaviour {
    [SerializeField] private GameObject objSliderMusic = null;
    [SerializeField] private GameObject objSliderSfx = null;
    
    private IEnumerator AsyncWhenLoadInit() {
        // wait time
        const float waitValue = 0.2f;
        yield return new WaitForSeconds(waitValue);
        
        // get script
        LocalStorageControl script = _localStorageControl;

        // init variables
        const int defaultInt = 50;
        _musicValue = script.GetByKey("musicVolume", defaultInt);
        _sfxValue = script.GetByKey("sfxVolume", defaultInt);

        // default control
        if (_musicValue == 0) _musicValue = defaultInt;
        if (_sfxValue == 0) _sfxValue = defaultInt;
        
        // render
        Debug.Log("_musicValue: " + _musicValue);
        Debug.Log("_sfxValue: " + _sfxValue);
        
        // slider set value
        objSliderMusic.GetComponent<Slider>().value = _musicValue;
        objSliderSfx.GetComponent<Slider>().value = _sfxValue;
        
        // sounds volume
        SoundsVolume();
    }

    private void SoundsVolume() {
        // get array
        Type type = typeof(SoundControl);
        if (!(FindObjectsOfType(type) is SoundControl[] arr)) return;
        
        // visit all
        foreach (SoundControl soundControl in arr) {
            string typeSound = soundControl.TypeGet();
            switch (typeSound) {
                case "MUSIC": soundControl.VolumeSet(_musicValue); break;
                case "SFX": soundControl.VolumeSet(_sfxValue); break;
            }
        }
    }
    
    private LocalStorageControl _localStorageControl = null;

    private void Start() {
        _localStorageControl = LocalStorageControl.GetScriptStorage();
        StartCoroutine( AsyncWhenLoadInit() );
    }

    private int _musicValue = 0;
    private int _sfxValue = 0;
    
    public void SliderChangedValue() {
        // get array of sliders
        Type type = typeof(SliderControl);
        if (!(FindObjectsOfType(type) is SliderControl[] arr)) return;
        if (arr.Length == 0) return;

        // get info from sliders
        foreach (SliderControl sliderControl in arr) {
            if (sliderControl.GetSliderType() == typeMusic) _musicValue = sliderControl.GetSliderVal();
            if (sliderControl.GetSliderType() == typeSfx) _sfxValue = sliderControl.GetSliderVal();
        }
        
        // render values
        RenderVolumesValues();
        
        // save to dictionary
        _localStorageControl.SaveToDictionary("musicVolume", _musicValue);
        _localStorageControl.SaveToDictionary("sfxVolume", _sfxValue);
        
        // sounds volume
        SoundsVolume();
    }

    private void RenderVolumesValues() {
        string message = $" === Music : {_musicValue} === Sfx : {_sfxValue} === ";
        Debug.Log(message);
    }
    
    private const string typeMusic = "MUSIC";
    private const string typeSfx = "SFX";
}
