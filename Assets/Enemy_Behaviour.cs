using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{

    // Enemy GameObject Information
    private Rigidbody2D enemy;
    private SpriteRenderer enemySprite;
    private Rigidbody2D robot;
    private SpriteRenderer youWin;

    // Enemy stats
    private int health = 5;
    private const float MoveSpeed = 7.5f;

    // Enemy Game Information
    private bool isFacingRight = false;
    private const float secondsToWait = 3.5f;
    private const int stopMovementDistance = 2;
    private float absoluteDistanceDifference;
    private bool waiting = false;

    void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        robot = GameObject.FindWithTag("Robot").GetComponent<Rigidbody2D>();
        youWin = GameObject.FindWithTag("Win Screen").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            // Player wins the game, stop everything
            enemySprite.enabled = false;
            youWin.sortingOrder = 2;
            Time.timeScale = 0;
        }

        if (waiting == false)
        {
            // Move left or right
            if (enemy.transform.position.x < robot.transform.position.x)
            {
                if (!isFacingRight) { Flip(); }
                enemy.velocity = new Vector2(1 * MoveSpeed, enemy.velocity.y);
            }
            else if (enemy.transform.position.x > robot.transform.position.x)
            {
                if (isFacingRight) { Flip(); }
                enemy.velocity = new Vector2(-1 * MoveSpeed, enemy.velocity.y);
            }

            absoluteDistanceDifference = Mathf.Abs(enemy.transform.position.x) - Mathf.Abs(robot.transform.position.x);
            // Stop when you get close to player to give them time to maneuver and attack
            if (absoluteDistanceDifference < stopMovementDistance)
            {
                StartCoroutine(waitAfterGettingtoPlayer(secondsToWait));
            }
        }
    }

    IEnumerator waitAfterGettingtoPlayer(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        waiting = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack_hitbox")
        {
            health--;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        Vector3 localPosition = transform.localPosition;
        localScale.x *= -1;
        if (isFacingRight == false)
        {
            localPosition.x -= 1;
        }
        else
        {
            localPosition.x += 1;
        }
        transform.localPosition = localPosition;
        transform.localScale = localScale;
    }
}
