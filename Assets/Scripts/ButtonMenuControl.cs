using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuControl : MonoBehaviour {
    [SerializeField] private string buttonText = string.Empty;

    public void SetButtonText(string textContent)
    {
        const string keyChildText = "Text";
        GameObject childText = gameObject.transform.Find(keyChildText).gameObject;
        Text script = childText.GetComponent<Text>();
        script.text = textContent.Trim();
        buttonText = textContent.Trim();
    }
    
    private void Start() {
        // set btn text
        const string keyChildText = "Text";
        GameObject childText = gameObject.transform.Find(keyChildText).gameObject;
        Text script = childText.GetComponent<Text>();
        script.text = buttonText;
        
        // get image component
        _imageComponent = GetComponent<Image>();
        
        // get children image
        const string keyChildImage = "Image";
        _childImage = gameObject.transform.Find(keyChildImage).gameObject;
        
        // rotate image
        const char operation = '+';
        RotateChildImage(operation);
    }

    private GameObject _childImage = null;

    private void RotateChildImage(char operation) {
        const float x = 0;
        const float y = 0;
        float z = (operation == '+') ? 45 : 0;
        _childImage.transform.eulerAngles = new Vector3(x, y, z);
    }
    
    private Image _imageComponent = null;

    public void ColorSimple() {
        if (!_imageComponent) return;
        _imageComponent.color = simpleColor;
        const char operation = '+';
        RotateChildImage(operation);
    }

    public void ColorHover() {
        if (!_imageComponent) return;
        _imageComponent.color = hoverColor;
        const char operation = '-';
        RotateChildImage(operation);
    }
    
    private readonly Color simpleColor = Color.white;
    private readonly Color hoverColor = Color.grey;
}
