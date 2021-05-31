using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationControl : MonoBehaviour {
    private Animator _animator = null;

    private int _hashIdle = 0;
    private int _hashRun = 0;
    private int _hashJump = 0;

    private int _hashCurrentState = 0;

    private void TriggerValueSet(int hashInteger) {
        if (_hashCurrentState == hashInteger) return;
        _animator.SetTrigger(hashInteger);
        _hashCurrentState = hashInteger;
    }
    
    private void Awake() {
        // animator
        _animator = gameObject.GetComponent<Animator>();
        // hash of states
        _hashIdle = Animator.StringToHash("should-hero-idle");
        _hashRun = Animator.StringToHash("should-hero-run");
        _hashJump = Animator.StringToHash("should-hero-jump");
    }

    public void AnimationHeroPlay(string animationKey) {
        switch (animationKey) {
            case "idle":
                TriggerValueSet(_hashIdle);
                return;
            case "run":
                TriggerValueSet(_hashRun);
                return;
            case "jump":
                TriggerValueSet(_hashJump);
                return;
        }
    }
}
