using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void Start()
    {
        // catch for non-collider objects
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
            print(gameObject.name + ": no collider");
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthController health = collision.gameObject.GetComponent<HealthController>();
        if (health != null)
            health.RemoveHealth(damage);
        if (tag == "Gun")
            Destroy(gameObject);
    }
}
