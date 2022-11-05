using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
/// This class is used to manage the image animator in Story and Challenge Mode Battle scenes .
///
/// It contains function to manage animator.
public class ImageCanvasAnimator : MonoBehaviour {
 
    public RuntimeAnimatorController controller; /**< unity placeholder to set the controller you want to use in the inspector*/
 
    Image imageCanvas; /**< unity placeholder for image */
    SpriteRenderer fakeRenderer; /**< unity placeholder for SpriteRenderer */
    Animator animator; /**< unity placeholder for Animator */
    [SerializeField] Sprite sprite; /**< unity placeholder for Sprite */
 
 
    /// This method is called whenever image is loaded.
    ///
    /// It initiates the Unity Animator for image
    void Start() {
        imageCanvas = gameObject.GetComponent<Image>();
        fakeRenderer = gameObject.AddComponent<SpriteRenderer>();
        // avoid the SpriteRenderer to be rendered
        fakeRenderer.enabled = false;
        animator = gameObject.AddComponent<Animator>();
 
        // set the controller
        animator.runtimeAnimatorController = controller;
    }
 
    /// This method is to trigger image animation.
    ///
    /// It initiates the call for rendering animation
    void Update() {
        // if a controller is running, set the sprite
        if (animator.runtimeAnimatorController) {
            sprite = fakeRenderer.sprite; // this assignment works
            imageCanvas.sprite = fakeRenderer.sprite; // this assignment DOES NOTHING
        }
    }

    /// This method is to trigger attack animation.
    ///
    /// It sets the trigger for attack animation
    void OnFire() {
        animator.SetTrigger("swordAttack");
    }
 
}