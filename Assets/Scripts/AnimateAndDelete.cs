using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndDelete : MonoBehaviour {
    [SerializeField] private Sprite [] arraySprites = null;
    [SerializeField] private float waitValue = 0;
        
    private SpriteRenderer _spriteRenderer = null;

    private void Start() {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine( AsyncCountAnimation() );
    }

    private IEnumerator AsyncCountAnimation() {
        foreach (Sprite sprite in arraySprites) {
            yield return new WaitForSeconds(waitValue);
            _spriteRenderer.sprite = sprite;
        }
        yield return new WaitForSeconds(waitValue);
        Destroy(gameObject);
    }
}
