using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int totalHealth = 10;

    private int currHealth;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = totalHealth;
    }

    public void RemoveHealth(int damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
            alive = false;
    }

    public bool IsAlive()
    {
        return alive;
    }
}
