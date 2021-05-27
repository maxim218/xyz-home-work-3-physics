using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour {
    private PlayerMoving _playerMovingScript = null;
    
    private void Start() {
        _playerMovingScript = gameObject.GetComponent<PlayerMoving>();
    }
    
    public void HorizontalInput(InputAction.CallbackContext context) {
        float value = context.ReadValue<float>();
        _playerMovingScript.SetDirectionX(value);
    }

    public void VerticalInput(InputAction.CallbackContext context) {
        float value = context.ReadValue<float>();
        _playerMovingScript.SetDirectionY(value);
    }
}
