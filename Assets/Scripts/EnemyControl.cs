using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    private SpriteRenderer _spriteRenderer = null;
    private GameObject _hero = null;
    private PlayerMoving _playerMoving = null;
    
    private void Start() {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        EnemyDangerousSet(true);
        _hero = GameObject.Find("Hero");
        _playerMoving = _hero.GetComponent<PlayerMoving>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != _hero) return;
        if (!_danger) return;
        Rigidbody2D rigidbodyHero = _hero.GetComponent<Rigidbody2D>();
        const float forceVertical = 150f;
        rigidbodyHero.AddForce(Vector2.up * forceVertical, ForceMode2D.Impulse);
        EnemyDangerousSet(false);
        _playerMoving.SubLive();
        StartCoroutine( WaitAndMakeDanger() );
    }

    private bool _danger = true;
    
    private void EnemyDangerousSet(bool dangerFlag) {
        _danger = dangerFlag;
        _spriteRenderer.color = _danger ? Color.red : Color.white;
    }

    private IEnumerator WaitAndMakeDanger() {
        const float waitTime = 0.2f;
        yield return new WaitForSeconds(waitTime);
        EnemyDangerousSet(true);
    }
}
