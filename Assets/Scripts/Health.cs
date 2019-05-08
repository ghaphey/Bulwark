using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 10;
    [SerializeField] private Slider healthSlider = null;
    //[SerializeField] private Text healthText = null;

    private int currHealth;
    private bool alive = true;

    public float currHealthPercent = 1f;
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
        if (healthSlider != null)
        {
            currHealthPercent = (float)currHealth / totalHealth;
            healthSlider.value = currHealthPercent;
        }
    }

    public bool IsAlive()
    {
        return alive;
    }
}
