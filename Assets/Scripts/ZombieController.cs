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

    private Transform goal = null;
    private NavMeshAgent agent = null;
    // enemy spawner will calculate the goal and assign it

    void Start()
    {
        myHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetGoal(Transform newGoal)
    {
        goal = newGoal;
        agent.SetDestination(goal.position);
        
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
        // TODO: stop movement when contact the wall
        //rb.MovePosition(new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y));
        //rb.AddForce(new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y));
    }

    private void Death()
    {
        print("Zombie Died");
        Destroy(gameObject);
    }
}
