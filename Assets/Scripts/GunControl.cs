using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {
    private Dictionary <string, Component> _dictionaryComponents = null;
    
    private void Start() {
        // init dictionary
        _dictionaryComponents = ComponentGetter.GetDictionaryComponents(gameObject);
        // async run
        StartCoroutine( AsyncAnimateAndFire() );
    }

    [SerializeField] private bool circleFlag = true;
    [SerializeField] private float waitFireValue = 0;
    [SerializeField] private Sprite [] spritesArray = null;

    private IEnumerator AsyncAnimateAndFire() {
        yield return new WaitForSeconds(waitFireValue);
        while (circleFlag) {
            for (int i = 0; i < spritesArray.Length; i++) {
                int index = i;
                if (index == 5) {
                    if (allowFire) Fire();
                }
                Sprite sprite = spritesArray[index];
                SpriteRenderer spriteRenderer = (SpriteRenderer) _dictionaryComponents["SpriteRenderer"];
                spriteRenderer.sprite = sprite;
                yield return new WaitForSeconds(waitFireValue);
            }
        }
    }

    [SerializeField] private GameObject bulletPrefab = null;
    
    private void Fire() {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        Vector3 position = Vector3.left * 0.8f;
        bullet.transform.position = transform.TransformPoint(position);
    }

    [SerializeField] private GameObject brokenGunPrefab = null;
    
    public void GunKill() {
        GameObject dead = Instantiate(brokenGunPrefab) as GameObject;
        dead.transform.position = transform.position;
        Destroy(gameObject);
    }

    [SerializeField] private bool allowFire = false;

    public void AllowFireSet(bool flag) {
        allowFire = flag;
    }
}
