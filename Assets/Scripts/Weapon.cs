using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header ("Weapon Properties")]
    [SerializeField] private float fireSpeed = 500f;
    [SerializeField] private int magSize = 10;
    [SerializeField] private int bullets = -1;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private float weaponOffset = 0.7f;
    [SerializeField] private GameObject shotPrefab = null;
    [Header("UI Properties")]
    [SerializeField] private GameObject ammoImage = null;
    [SerializeField] private float widthOffset = 40f;
    //[SerializeField] private float heightOffset = 60f;


    //private RectTransform ammoPanel = null;
    private int currMag = 0;
    //private List<GameObject> ammoImages = null;
    //private Text reloadText = null;
    //private float reloadBarWidth = 0f;
    private ParticleSystem fireFx = null;

    void Start()
    {
        //ammoPanel = GameObject.FindGameObjectWithTag("Ammo").GetComponent<RectTransform>();
        //transform.localPosition = new Vector3(weaponOffset, 0f);
        
        //reloadBarWidth = ammoPanel.rect.width;
        //ammoImages = new List<GameObject>();
        //PopulateAmmoPanel(0);
        currMag = magSize;

        fireFx = GetComponent<ParticleSystem>();
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public float GetWeaponOffset()
    {
        return weaponOffset;
    }

    public float GetWidthOffset()
    {
        return widthOffset;
    }

    public GameObject GetAmmoImage()
    {
        return ammoImage;
    }

    public int GetMagSize()
    {
        return magSize;
    }

    public int GetCurrMag()
    {
        return currMag;
    }

    public void Reload()
    {
        if (bullets > magSize || bullets == -1)
            currMag = magSize;
        else
            currMag = bullets;
    }

    public void AddAmmunition(int add)
    {
        bullets += add;
    }

    public int GetAmmunitionCount()
    {
        return bullets;
    }

    public float AdjustReloadBar(float currTimer)
    {
        return currTimer / reloadTime;
    }

    public int Fire()
    {
        if (currMag > 0)
        {
            currMag--;
            
            GameObject shot = Instantiate(shotPrefab, transform) as GameObject;
            fireFx.Play();
            shot.transform.rotation = transform.rotation;
            shot.transform.localPosition += new Vector3(weaponOffset, 0f);
            // TODO: add relative velocity of player for consistant projectile speed
            shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(fireSpeed, 0.0f));
            shot.transform.SetParent(transform.parent.parent);
            // set parent as world
            if (bullets != -1)
            {
                bullets--;
                if (bullets == 0)
                    DiscardWeapon();
            }
        }
        return currMag;
    }

    private void DiscardWeapon()
    {
        Destroy(gameObject);
    }
}
