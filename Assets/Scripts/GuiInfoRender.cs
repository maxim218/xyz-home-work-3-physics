using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiInfoRender : MonoBehaviour {
    private HeroControl _heroControl = null;
    private ControlHealth _controlHealth = null;

    private void Start() {
        _heroControl = gameObject.GetComponent<HeroControl>();
        _controlHealth = gameObject.GetComponent<ControlHealth>();
    }

    private void OnGUI() {
        int money = _heroControl.GetSumValue();
        GUI.Box(new Rect(110, 30, 100, 30), "Money: " + money);

        int health = _controlHealth.Lives;
        GUI.Box(new Rect(220, 30, 100, 30), "Health: " + health);
    }
}
