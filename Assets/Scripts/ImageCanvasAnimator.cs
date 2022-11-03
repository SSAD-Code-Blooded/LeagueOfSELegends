using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class ImageCanvasAnimator : MonoBehaviour {
 
    // set the controller you want to use in the inspector
    public RuntimeAnimatorController controller;
 
    // the UI/Image component
    Image imageCanvas;
    // the fake SpriteRenderer
    SpriteRenderer fakeRenderer;
    // the Animator
    Animator animator;
    [SerializeField] Sprite sprite;
 
 
    void Start() {
        imageCanvas = gameObject.GetComponent<Image>();
        fakeRenderer = gameObject.AddComponent<SpriteRenderer>();
        // avoid the SpriteRenderer to be rendered
        fakeRenderer.enabled = false;
        animator = gameObject.AddComponent<Animator>();
 
        // set the controller
        animator.runtimeAnimatorController = controller;
    }
 
    void Update() {
        // if a controller is running, set the sprite
        if (animator.runtimeAnimatorController) {
            sprite = fakeRenderer.sprite; // this assignment works
            imageCanvas.sprite = fakeRenderer.sprite; // this assignment DOES NOTHING
        }
    }

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }
 
}