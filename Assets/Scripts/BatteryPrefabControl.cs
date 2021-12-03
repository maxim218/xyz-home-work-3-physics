using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPrefabControl : MonoBehaviour
{
    [SerializeField] private string heroName = string.Empty;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Trim() == heroName)
        {
            LampHeroControl script = FindObjectOfType<LampHeroControl>();
            if (script != null) script.SetEnergyNumber(1);
            Destroy(gameObject);
        }
    }
}
