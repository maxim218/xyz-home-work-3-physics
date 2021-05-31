using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimationControl : MonoBehaviour {
    [SerializeField] private float waitFloat = 0f;
    
    [SerializeField] private bool playFlag = false;
    
    [SerializeField] private Sprite [] arraySprites;

    [SerializeField] private int [] integerNumbers;

    private SpriteRenderer _spriteRenderer = null;
    
    private void Start() {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine( Counting() );
    }

    private IEnumerator Counting() {
        while (playFlag) {
            foreach (int index in integerNumbers) {
                _spriteRenderer.sprite = arraySprites[index];
                yield return new WaitForSeconds(waitFloat);
            }
        }
    }
}
