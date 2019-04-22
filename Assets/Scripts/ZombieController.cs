﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private HealthController myHealth;
    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponent<HealthController>();
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
        transform.position += new Vector3(speed * Time.deltaTime, 0);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}