using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 10;
    [SerializeField] private Text healthText = null;

    private int currHealth;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = totalHealth;
        if (healthText != null)
            healthText.text = totalHealth.ToString();
    }

    public void RemoveHealth(int damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
            alive = false;
        if (healthText != null)
            healthText.text = currHealth.ToString();
    }

    public bool IsAlive()
    {
        return alive;
    }
}
