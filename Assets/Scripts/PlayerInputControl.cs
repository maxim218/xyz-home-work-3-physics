using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour {
    private PlayerMoving _playerMovingScript = null;
    
    private void Start() {
        _playerMovingScript = gameObject.GetComponent<PlayerMoving>();
    }
    
    public void HorizontalInput(InputAction.CallbackContext context)
    {
        return;
        float value = context.ReadValue<float>();
        _playerMovingScript.SetDirectionX(value);
    }

    public void VerticalInput(InputAction.CallbackContext context)
    {
        return;
        float value = context.ReadValue<float>();
        _playerMovingScript.SetDirectionY(value);
    }

    public void AndroidInput(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();

        float x = Mathf.Round(vec.x);
        float y = Mathf.Round(vec.y);

        _playerMovingScript.SetDirectionX(x);
        _playerMovingScript.SetDirectionY(y);
    }
}
