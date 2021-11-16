using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopHitController : MonoBehaviour
{
    [SerializeField] private GameObject hero = null;

    [SerializeField] private UnityEvent showEvent = null;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != hero)
            return;

        showEvent?.Invoke();
    }
}
