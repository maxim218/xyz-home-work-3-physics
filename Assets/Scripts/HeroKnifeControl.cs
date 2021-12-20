using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroKnifeControl : MonoBehaviour {
    [SerializeField] private Color colorValue = Color.white;
    [SerializeField] private GameObject attackSwordPrefab = null;
    public void CatchKnife() {
        // hero change color
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorValue;
        // give sword
        _isHeroHasSword = true;
    }

    private bool _isHeroHasSword = false;
    
    public void AttackInput(InputAction.CallbackContext context) {
        // control hero has sword
        if (!_isHeroHasSword) return;
        
        // control prohibit
        if (!_allowCreating) return;
        
        // create sword
        GameObject sword = Instantiate(attackSwordPrefab) as GameObject;
        sword.transform.position = transform.position;
        
        // prohibit creating
        _allowCreating = false;
    }

    public void AndroidAttackControl()
    {
        // control hero has sword
        if (!_isHeroHasSword) return;
        
        // control prohibit
        if (!_allowCreating) return;
        
        // create sword
        GameObject sword = Instantiate(attackSwordPrefab) as GameObject;
        sword.transform.position = transform.position;
        
        // prohibit creating
        _allowCreating = false;
    }
    
    public void AllowCreatingYes() {
        _allowCreating = true;
    }
    
    private bool _allowCreating = true;
}
