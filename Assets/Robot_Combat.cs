using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Robot_Combat : MonoBehaviour
{

    // Game Information
    private Animator animator;
    private BoxCollider2D attackHitbox;
    private SpriteRenderer health1;
    private SpriteRenderer health2;
    private SpriteRenderer health3;
    private SpriteRenderer gameOver;

    // Character Information
    private int health = 3;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackHitbox = GameObject.FindWithTag("attack_hitbox").GetComponent<BoxCollider2D>();
        attackHitbox.enabled = false;
        health1 = GameObject.FindWithTag("health1").GetComponent<SpriteRenderer>();
        health2 = GameObject.FindWithTag("health2").GetComponent<SpriteRenderer>();
        health3 = GameObject.FindWithTag("health3").GetComponent<SpriteRenderer>();
        gameOver = GameObject.FindWithTag("Game Over Screen").GetComponent<SpriteRenderer>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        animator.SetTrigger("attack");
    }

    private void EnableAttackHitbox()
    {
        attackHitbox.enabled=true;
    }

    private void DisableAttackHitbox()
    {
        attackHitbox.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health--;
            switch(health)
            {
                case -1:
                    // Game is over, you lose the game.
                    gameOver.sortingOrder = 2;
                    Time.timeScale = 0;
                    break;
                case 0:
                    health1.enabled = false;
                    break;
                case 1:
                    health2.enabled = false;
                    break;
                case 2:
                    health3.enabled = false;
                    break;
            }
        }
    }
}
