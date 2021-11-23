using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallControl : MonoBehaviour
{
    public void SetPosition(float x, float y)
    {
        const int z = 8;
        transform.position = new Vector3(x, y, z);
    }
    
    public void SetScale(float scale)
    {
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        transform.localScale = scaleVector;
    }
}
