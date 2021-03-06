﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Collider2D attack = null;
    [SerializeField] private float attackLen = 0.5f;
    [SerializeField] private float hitRate = 1.5f;
    private Health myHealth;
    private Rigidbody2D rb;

    private int playerTeamContact = 0;
    private bool attacking = false;

    private float animTimer = 0f;
    private float attackTimer = 0f;

    private Animator anim = null;

    private int maneuver = 0;
    private int updown = 1;

    void Start()
    {
        myHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(myHealth.IsAlive())
        {
            Move();
            if(attacking)
                Attack();
        }
        else
        {
            Death();
        }
    }


    private void Move()
    {
        if (playerTeamContact > 0)
        {
            anim.SetBool("moving", false);
            return;
        }
        else
        {
            anim.SetBool("moving", true);
            if(maneuver <= 0)
                rb.MovePosition(new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y));
            else
                rb.MovePosition(new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y + speed * Time.fixedDeltaTime * updown));

        }
    }


    private void Attack()
    {
        if (attackTimer <= 0f)
        {
            if (!attack.enabled)
            {
                attack.enabled = true;
                anim.SetBool("attacking", true);
                animTimer = attackLen;
            }
            bool complete = ThrowAttack();
            if (complete)
            {
                anim.SetBool("attacking", false);
                attackTimer = hitRate;
                attack.enabled = false;
            }
        }
        else
            attackTimer -= Time.deltaTime;
    }

    private bool ThrowAttack()
    {
        if (animTimer <= 0f)
        {
            return true;
        }
        else
        {
            animTimer -= Time.deltaTime;
            return false;
        }
    }

    private void Death()
    {
        print("Zombie Died: " + myHealth.IsAlive().ToString());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Damage damage = collision.gameObject.GetComponent<Damage>();
            if (damage == null)
            {
                attacking = true;
                playerTeamContact++;
            }
        }
        
        if (collision.transform.tag == "Enemy")
        {
            maneuver++;
            if (collision.transform.position.y > transform.position.y)
                updown = -1;
            else
                updown = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerTeamContact--;
            attacking = false;
            attackTimer = 0f;
            animTimer = 0f;
        }
        
        if (collision.transform.tag == "Enemy")
            maneuver--;
    }


    // NEED TO ADD MOVEMENT BEHAVIORS HERE, AND CHANGE ONLY ATTACK TO CAUSE DAMAGE
}
