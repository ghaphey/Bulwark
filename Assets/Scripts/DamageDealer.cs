using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 15f;

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

    public float GetSpeed()
    {
        return speed;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
