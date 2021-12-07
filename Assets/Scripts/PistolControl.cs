using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : MonoBehaviour {
   private GameObject _hero = null;

   private void Start() {
      // get hero
      HeroControl heroControl = (HeroControl)FindObjectOfType(typeof(HeroControl));
      _hero = heroControl.gameObject;
      // fire start
      StartCoroutine( AsyncFire() );
   }

   [SerializeField] private bool circleFire = true;

   [SerializeField] private float startDelayValue = 0f;
   
   private IEnumerator AsyncFire() {
      yield return new WaitForSeconds(startDelayValue);
      while (circleFire) {
         const float waitValue = 1f;
         yield return new WaitForSeconds(waitValue);
         GameObject bullet = CreateBullet(transform.position);
         FireForce(bullet);
      }
   }

   [SerializeField] private GameObject bulletPrefab = null;
   
   private GameObject CreateBullet(Vector3 position) {
      GameObject bullet = Instantiate(bulletPrefab) as GameObject;
      bullet.transform.position = position;
      return bullet;
   }

   [SerializeField] private float forceModule = 1100f;
   
   private void FireForce(GameObject bullet) {
      bullet.transform.up = gameObject.transform.up;
      Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
      Vector2 vectorFire = (Vector2) (bullet.transform.up * (-1 * forceModule));
      bulletRigidbody2D.AddForce(vectorFire);
   }
   
   private void LateUpdate() {
      LookToGoal(_hero);
   }

   private void LookToGoal(GameObject goal) {
      Vector2 posA = (Vector2) goal.transform.position;
      Vector2 posB = (Vector2) gameObject.transform.position;
      Vector2 posDelta = posA - posB;
      transform.up = (Vector3) (-1 * posDelta);
   }
}
