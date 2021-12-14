using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour {
    private GameObject _hero = null;

    private PistolControl _pistolControl = null;

    private IEnumerator _coroutine = null;

    private void StopTiming()
    {
        try
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        catch
        {
            const string wrnMsg = "Stop timing warning";
            Debug.LogWarning(wrnMsg);
        }
    }
    
    private PistolControl GetPistolControl()
    {
        if (_pistolControl == null) 
            _pistolControl = FindObjectOfType<PistolControl>();
        return _pistolControl;
    }

    private void HeroGet()
    {
        if(null == _hero)
        {
            HeroControl heroControl = (HeroControl) FindObjectOfType(typeof(HeroControl));
            _hero = heroControl.gameObject;
        }
    }
    
    private void Start()
    {
        HeroGet();
    }

    private void OnEnable()
    {
        StopTiming();
        _coroutine = KillStar();
        StartCoroutine( _coroutine );
    }

    private IEnumerator KillStar() {
        const float waitValue = 2.5f;
        yield return new WaitForSeconds(waitValue);
        UsePool();
    }

    private void UsePool()
    {
        // using pool
        StopTiming();
        PistolControl.InitBullet(gameObject, Vector3.zero, false);
        PistolControl script = GetPistolControl();
        if (script) 
            script.QueueAdd(this);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != _hero) return;
        ControlHealth controlHealth = _hero.GetComponent<ControlHealth>();
        controlHealth.AddLives(-1);
        UsePool();
    }
}
