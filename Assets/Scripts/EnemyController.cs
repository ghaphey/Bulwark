using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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

        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
