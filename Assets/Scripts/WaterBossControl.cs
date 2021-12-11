using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossControl : MonoBehaviour
{
    [SerializeField] private Animator animatorComponent = null;
    [SerializeField] private ControlHealth controlHealthComponent = null;
    
    private void RunWaterAnimation(string stateName)
    {
        if (animatorComponent != null)
        {
            const int waitTime = 0;
            animatorComponent.CrossFade(stateName, waitTime);
        }
    }

    public void WaterAttack()
    {
        const string waterState = "WaterAnimationState";
        RunWaterAnimation(waterState);
    }

    public void WaterWaitIdle()
    {
        const string idleState = "Idle";
        RunWaterAnimation(idleState);
    }

    private void Start()
    {
        WaterWaitIdle();
        StartCoroutine( AsyncHitHeroInTime() );
    }

    private IEnumerator AsyncHitHeroInTime()
    {
        while (true)
        {
            // waiting
            const float waitValue = 0.35f;
            yield return new WaitForSeconds(waitValue);
            
            // control hit
            if (controlHealthComponent != null)
            {
                if (controlHealthComponent.Lives > 0)
                {
                    float waterY = transform.position.y;
                    float heroY = controlHealthComponent.gameObject.transform.position.y;
                    if (waterY > heroY) controlHealthComponent.AddLives(-1);
                }
            }
        }
    }
}
