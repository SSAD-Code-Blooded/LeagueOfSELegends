using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// This class is used to manage the image animation in Story and Challenge Mode Battle scenes .
///
/// It contains function to enable image animations.
public class ImageAnimation : MonoBehaviour {

	public Sprite[] sprites; /**< unity placeholder for sprites */
	public int spritePerFrame = 6; /**< variable to set spritePerFrame*/
	public bool loop = true; /**< boolean to set animation loop*/
	public bool destroyOnEnd = false; /**< boolean to stop animation loop*/

	private int index = 0; /**< variable to get index of sprite*/
	private Image image; /**< unity placeholder for image */
	private int frame = 0;  /**< variable to get index of animation frame*/

    /// This method is called whenever image is loaded.
    ///
    /// It loads Sprite Image
	void Awake() {
		image = GetComponent<Image> ();
	}

    /// This method is called to trigger sprite animation.
    ///
    /// It loads the next animation frame
	void Update () {
		if (!loop && index == sprites.Length) return;
		frame ++;
		if (frame < spritePerFrame) return;
		image.sprite = sprites [index];
		frame = 0;
		index ++;
		if (index >= sprites.Length) {
			if (loop) index = 0;
			if (destroyOnEnd) Destroy (gameObject);
		}
	}
}