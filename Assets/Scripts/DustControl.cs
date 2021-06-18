using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustControl : MonoBehaviour {
    public void DeleteDust() {
        Destroy(gameObject);
    }

    public void DeleteDustAfterTime() {
        StartCoroutine( WaitAndDeleteDust() );
    }

    [SerializeField] private float waitTime = 0.25f;
    
    private IEnumerator WaitAndDeleteDust() {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
