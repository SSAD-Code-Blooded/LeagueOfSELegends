using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// This class is used to manage the slime sprite while in Story Mode Battle scene.
///
/// It manages slime animation.
public class Enemy : MonoBehaviour
{
    Animator animator; /**< unity placeholder for Animator */

    /**< Sets Defeated trigger for slime when health=0 */
    public float Health {
        set {
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    public float health = 1; /**< variable for slime health */

    /// This method is to initiate slime.
    ///
    /// It fetched the slime Animator.
    private void Start() {
        animator = GetComponent<Animator>();
    }

    /// This method is to set Defeated trigger for slime animation.
    ///
    /// It sets the Defeated triger.
    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    /// This method is used to remove slime from scene.
    ///
    /// It removes slime when it is defeated.
    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
