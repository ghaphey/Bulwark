using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject attack = null;
    [SerializeField] private float attackLen = 0.5f;
    [SerializeField] private float hitRate = 1.5f;
    private Health myHealth;
    private Rigidbody2D rb;

    private bool playerTeamContact = false;
    private bool attacking = false;

    private float animTimer = 0f;
    private float attackTimer = 0f;

    private Animator anim = null;


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
        if (playerTeamContact == true)
            return;
        else
        {
            rb.MovePosition(new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y));
            //rb.AddForce(new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y));
        }
    }


    private void Attack()
    {
        if (attackTimer <= 0f)
        {
            attack.SetActive(true);
            bool complete = ThrowAttack();
            if (complete)
            {
                attackTimer = hitRate;
                attack.SetActive(false);
            }
        }
        else
            attackTimer -= Time.deltaTime;
    }

    private void ThrowAttack()
    {
        if (animTimer <= 0f)
        {
            // TODO: must play animation and keep attack hit box up
            // until the animation is complete, then turn off until
            // the next hit cycle
            anim.SetBool("attacking", true);
            animTimer = attackLen;
        }
        else
        {
            animTimer -= Time.deltaTime;
        }
    }

    public void HaltAttack()
    {
        attack.SetActive(false);
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
            if (damage != null)
            {
                playerTeamContact = false;
            }
            else
            {
                attacking = true;
                playerTeamContact = true;
            }
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerTeamContact = false;
            attacking = false;
            attackTimer = 0f;
            animTimer = 0f;
        }
    }


    // NEED TO ADD MOVEMENT BEHAVIORS HERE, AND CHANGE ONLY ATTACK TO CAUSE DAMAGE
}
