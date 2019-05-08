using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Health myHealth;
    private Rigidbody2D rb;

    private bool playerTeamContact = false;

    void Start()
    {
        myHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(myHealth.IsAlive())
        {
            Move();
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

    private void Death()
    {
        print("Zombie Died: " + myHealth.IsAlive().ToString());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Damage projectile = collision.gameObject.GetComponent<Damage>();
            if (projectile != null)
            {
                if (projectile.projectile)
                {
                    playerTeamContact = false;
                    return;
                }
            }
            playerTeamContact = true;
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerTeamContact = false;
            //print("out");
        }
    }
}
