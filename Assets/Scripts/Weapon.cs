using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireSpeed = 500f;
    [SerializeField] private int magSize = 10;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private float weaponOffset = 0.7f;
    [SerializeField] private GameObject shotPrefab = null;

    private int currMag = 0;

    void Start()
    {
        currMag = magSize;
        transform.localPosition = new Vector3(weaponOffset, 0f);
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public void Reload()
    {
        currMag = magSize;
    }

    public bool Fire()
    {
        if (currMag <= 0)
        {
            print("Empty Magazine");
            //TODO: Print UI Message
            return false;
        }
        else
        {
            currMag--;
            GameObject shot = Instantiate(shotPrefab, transform) as GameObject;
            shot.transform.rotation = transform.rotation;
            shot.transform.localPosition += new Vector3(weaponOffset, 0f);
            // TODO: add relative velocity of player for consistant projectile speed
            shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(fireSpeed, 0.0f));
            shot.transform.SetParent(transform.parent.parent);
            // set parent as world
            return true;
        }
    }
}
