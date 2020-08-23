using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] public bool projectile = false;
    [SerializeField] private GameObject hitFXPrefab;

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
            //print(collision.gameObject.name + " hit by " + gameObject.name);
            collidedHealth.RemoveHealth(damage);
            if (hitFXPrefab != null)
            {
                GameObject newFX = Instantiate(hitFXPrefab);
                newFX.transform.position = transform.position;
                newFX.GetComponent<ParticleSystem>().Play();
                Destroy(newFX, 1.5f);
            }
        }
        if (projectile)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedHealth = collision.gameObject.GetComponent<Health>();
        if (collidedHealth != null && (collision.gameObject.tag != gameObject.tag))
        {
            collidedHealth.RemoveHealth(damage);
        }
    }

}
