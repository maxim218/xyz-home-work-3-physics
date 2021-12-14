using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : MonoBehaviour {
   private GameObject _hero = null;

   private Queue<StarControl> _starsQueue = null;

   private void Awake()
   {
      _starsQueue = new Queue<StarControl>();
   }

   public void QueueAdd(StarControl starControl)
   {
      _starsQueue.Enqueue(starControl);
   }
   
   private static void ZeroPhysics(GameObject bullet)
   {
      Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
      bulletRigidbody2D.velocity = Vector2.zero;
      bulletRigidbody2D.angularVelocity = 0;
   }
   
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
   
   public static GameObject InitBullet(GameObject bullet, Vector3 position, bool activeFlag)
   {
      ZeroPhysics(bullet);
      bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
      bullet.transform.position = position;
      bullet.SetActive(activeFlag);
      return bullet;
   }
   
   private GameObject CreateBullet(Vector3 position) {
      if (_starsQueue.Count > 0)
      {
         StarControl starControl = _starsQueue.Dequeue();
         return InitBullet(starControl.gameObject, position, true);
      } 
      else
      {
         GameObject bullet = Instantiate(bulletPrefab) as GameObject;
         return InitBullet(bullet, position, true);
      }
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
