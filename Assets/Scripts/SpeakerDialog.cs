using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerDialog : MonoBehaviour
{
    [SerializeField] private GameObject hero = null;
    
    [SerializeField] private DialogController dialogControllerComponent = null;
    
    [SerializeField] private string dialogName = string.Empty;

    private bool _allowDialog = true;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != hero)
            return;

        if (!_allowDialog)
            return;

        _allowDialog = false;

        dialogControllerComponent.SetCurrentDialog(dialogName);
        dialogControllerComponent.DialogBegin();
    }

    private void Start()
    {
        LocalizationDontDestroy script = (LocalizationDontDestroy)FindObjectOfType(typeof(LocalizationDontDestroy));
        if (script) {
            if (script.GetLocalizationType() == "RUS") {
                dialogName += "_rus";
            }
        }
    }
}
