using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D ourController;
    public Animator animator;
    [SerializeField] public float movementSpeed;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public int health = 50;
    public GameObject deathEffect;
    public Rigidbody2D rd;
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsCrouching", crouch);

        if(Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if(Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    public void OnLanding() {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching) {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Enemy hit");
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("Current health" + health);
        

        if (health <= 0) {
            Die();
        }
    }
    void Die() {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void FixedUpdate() {
        ourController.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
