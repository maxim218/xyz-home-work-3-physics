using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCreatingFireBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballFirePrefab = null;

    public void CreateFireBall()
    {
        GameObject fireBall = Instantiate(ballFirePrefab) as GameObject;
        FireBallControl component = fireBall.GetComponent<FireBallControl>();
        Vector3 position = transform.position;
        component.SetPosition(position.x, position.y);
        component.SetScale(_scale);
    }

    private float _scale = 1;

    public void PerkIncreaseScale(float value)
    {
        _scale += value;
    }
}
