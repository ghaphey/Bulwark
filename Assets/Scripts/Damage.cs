using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitRate = 10;
    [SerializeField] private bool projectile = false;

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
        if (collidedHealth != null)
        {
            if (collision.gameObject.tag != gameObject.tag)
                collidedHealth.RemoveHealth(damage);
        }
        if (projectile)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collidedHealth != null)
        {
            count += Time.deltaTime;
            if (count >= hitRate)
            {
                collidedHealth.RemoveHealth(damage);
                count = 0f;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        count = 0f;
        collidedHealth = null;
    }
}
