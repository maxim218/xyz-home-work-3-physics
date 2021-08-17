using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInWallControl : MonoBehaviour {
   [SerializeField] private float liveTime = 0;

   private void Start() {
      StartCoroutine( KillMe() );
   }
   
   private IEnumerator KillMe() {
      yield return new WaitForSeconds(liveTime);
      Destroy(gameObject);
   }
}
