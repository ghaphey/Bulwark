using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitRate = 10;
    [SerializeField] public bool projectile = false;

    private float count = 0f;
    private Health collidedHealth = null;

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
        collidedHealth = collision.gameObject.GetComponent<Health>();
        if (collidedHealth != null && (collision.gameObject.tag != gameObject.tag))
        {
            print(collision.gameObject.name + " hit by " + gameObject.name);
            collidedHealth.RemoveHealth(damage);
        }
        else
            collidedHealth = null;
        if (projectile)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collidedHealth != null && (collision.gameObject.tag != gameObject.tag))
        {
            count += Time.deltaTime;
            if (count >= hitRate)
            {
                print(collision.gameObject.name + " hit by " + gameObject.name);
                collidedHealth.RemoveHealth(damage);
                count = 0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        count = 0f;
        collidedHealth = null;
    }
}
