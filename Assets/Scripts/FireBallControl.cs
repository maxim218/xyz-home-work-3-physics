using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallControl : MonoBehaviour
{
    private float _scale = 1;
    
    public void SetPosition(float x, float y)
    {
        const int z = 8;
        transform.position = new Vector3(x, y, z);
    }
    
    public void SetScale(float scale)
    {
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        transform.localScale = scaleVector;
        _scale = scale;
    }

    public void MakeFlyNearObjects()
    {
        Type type = typeof(GranateControl);
        GranateControl[] arr = FindObjectsOfType(type) as GranateControl[];

        if (arr == null)
            return;
        
        foreach (GranateControl granateControl in arr)
        {
            if (granateControl)
            {
                Vector2 fireBallPos = (Vector2) transform.position;
                Vector2 granatePos = (Vector2) granateControl.gameObject.transform.position;
                float distance = Vector2.Distance(fireBallPos, granatePos);
                if(distance < _scale)
                {
                    granateControl.BeginFlying();
                }
            }
        }
    }

    public void KillAfterTime()
    {
        StartCoroutine( AsyncKillMe() );
    }

    private IEnumerator AsyncKillMe()
    {
        const float waitTime = 0.4f;
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
