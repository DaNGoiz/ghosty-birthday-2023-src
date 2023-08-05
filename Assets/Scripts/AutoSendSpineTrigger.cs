using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSendSpineTrigger : MonoBehaviour {

    public float time = 5f;
    private bool isDancing = false;
    private bool hasClicked = false;
    private bool continueClicking = false;

    private Animator skeletonAnimation;
    private void Awake() {
        skeletonAnimation = GetComponent<Animator>();
    }
    
    public void Click(){
        if(!hasClicked){
            hasClicked = true;
            OnDance();
        }
    }

    public void ContinueClicking(){
        continueClicking = true;
    }

    void ClickCountDown(){
        time -= Time.deltaTime;
        if (time <= 0) {
            time = 5f;
            continueClicking = false;
        }
    }

    public void OnDance(){
        skeletonAnimation.SetTrigger("Dance");
        isDancing = true;
    }

    public void OnIdle(){
        isDancing = false;
        hasClicked = false;
        if(continueClicking){
            CountDown();
        }
        else{
            skeletonAnimation.SetTrigger("Idle");
        }
    }

    private void CountDown() {
        time -= Time.deltaTime;
        if (time <= 0) {
            time = 5f;
            OnIdle();
        }
    }

    private void Update() {
        if(isDancing) CountDown();
        if(continueClicking) ClickCountDown();
    }

}