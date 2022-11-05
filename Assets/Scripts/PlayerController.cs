using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// This class is used to manage the player sprite while in World scene.
///
/// It manages player movement while in World scene.
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f; /**< variable to set player movespeed */
    public float collisionOffset = 0.05f; /**< variable to set width of collision contact */
    public ContactFilter2D movementFilter; /**< manages collision along with 2D collider */

    Vector2 movementInput; /**< unity placeholder for movementInput */
    SpriteRenderer spriteRenderer; /**< unity placeholder for SpriteRenderer */
    Rigidbody2D rb; /**< unity placeholder for Rigidbody2D */
    Animator animator; /**< unity placeholder for Animator */
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); /**< unity placeholder for RaycastHit2D */

    bool canMove = true; /**< boolean to set if player is moving already */

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); /**< fetch Rigidbody2D from scene */
        animator = GetComponent<Animator>(); /**< fetch Animator from scene */
        spriteRenderer = GetComponent<SpriteRenderer>(); /**< fetch SpriteRenderer from scene */
    }

    /// This method is called whenever users inputs a directional key.
    ///
    /// It updates isMoving status and checks whether the sprite should be flipped horizontally.
    private void FixedUpdate() {
        if(canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero){
                
                bool success = TryMove(movementInput);

                if(!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                
                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }

    /// This method is called whenever users inputs a directional key.
    ///
    /// It renders the moving animation and next location of the Player.
    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
        
    }

    /// This method is called whenever users inputs a directional key.
    ///
    /// It gets the current movementValue of the player.
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    /// This method is called whenever users clicks on the screen.
    ///
    /// It triggers the swordAttack animation.
    void OnFire() {
        animator.SetTrigger("swordAttack");
    }

    /// This method is called to prevent movement animation.
    ///
    /// It is set when user is rendering attack animation.
    public void LockMovement() {
        canMove = false;
    }

    /// This method is called to allow movement animation.
    ///
    /// It is set when user has finished attack animation.
    public void UnlockMovement() {
        canMove = true;
    }
}
