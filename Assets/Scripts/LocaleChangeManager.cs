using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonToTranslate
{
    public ButtonMenuControl buttonMenuControl = null;
    public string key = string.Empty;
}

[Serializable]
public class LabelToTranslate
{
    public Text textComponent = null;
    public string key = string.Empty;
}

[Serializable]
public class SliderToTranslate
{
    public SliderControl sliderControl = null;
    public string key = string.Empty;
}

public class LocaleChangeManager : MonoBehaviour
{
    [SerializeField] private string localizationType = string.Empty;

    [SerializeField] private ButtonToTranslate[] buttonsArray = null;

    [SerializeField] private LabelToTranslate[] labelsArray = null;

    [SerializeField] private SliderToTranslate[] slidersArray = null;
    
    [ContextMenu("Translate All Method")]
    public void TranslateAll()
    {
        WordsStorage wordsStorage = LocaleManager.GetLocaleStorage(localizationType);

        foreach (ButtonToTranslate buttonToTranslate in buttonsArray)
        {
            string translate = LocaleManager.TranslateByKey(wordsStorage, buttonToTranslate.key);
            buttonToTranslate.buttonMenuControl.SetButtonText(translate);
        }

        foreach (LabelToTranslate labelToTranslate in labelsArray)
        {
            string translate = LocaleManager.TranslateByKey(wordsStorage, labelToTranslate.key);
            labelToTranslate.textComponent.text = translate;
        }

        foreach (SliderToTranslate sliderToTranslate in slidersArray)
        {
            string translate = LocaleManager.TranslateByKey(wordsStorage, sliderToTranslate.key);
            sliderToTranslate.sliderControl.SetPrefixString(translate);
        }
    }

    public void ClickChangeLocalization(string type)
    {
        string message = "Type: " + type;
        Debug.Log(message);

        localizationType = type;
        TranslateAll();

        SetLocalizationType(localizationType);
    }

    private static void SetLocalizationType(string type) {
        LocalizationDontDestroy script = (LocalizationDontDestroy)FindObjectOfType(typeof(LocalizationDontDestroy));
        if (script) {
            script.SetLocalizationTypeStore(type);
        }
    }
}
