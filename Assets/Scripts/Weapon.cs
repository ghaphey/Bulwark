using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private RectTransform ammoPanel = null;
    private int currMag = 0;
    private List<GameObject> ammoImages = null;
    private Text reloadText = null;
    private float reloadBarWidth = 0f;
    private ParticleSystem fireFx = null;

    void Start()
    {
        ammoPanel = GameObject.FindGameObjectWithTag("Ammo").GetComponent<RectTransform>();
        transform.localPosition = new Vector3(weaponOffset, 0f);
        reloadText = ammoPanel.GetComponentInChildren<Text>();
        ResetReloadText();
        reloadBarWidth = ammoPanel.rect.width;
        ammoImages = new List<GameObject>();
        PopulateAmmoPanel(0);
        currMag = magSize;
        DeactivateAmmoPanel();

        fireFx = GetComponent<ParticleSystem>();
    }

    public void PopulateAmmoPanel(int currIndex)
    {
        for (int i = currIndex; i < magSize; i++)
        {
            ammoImages.Add(Instantiate(ammoImage, ammoPanel));
            RectTransform nAmmo = ammoImages[i].GetComponent<RectTransform>();
            nAmmo.Translate(new Vector3(widthOffset * -i * 1.25f, 0f));
        }
    }

    public void DeactivateAmmoPanel()
    {
        for (int i = 0; i < currMag; i++)
        {
            ammoImages[i].SetActive(false);
        }
    }

    public void ActivateAmmoPanel()
    {
        for (int i = 0; i < currMag; i++)
        {
            ammoImages[i].SetActive(true);
        }
    }

    private void ReloadAmmoPanel(int currIndex)
    {
        for (int i = currIndex; i < magSize; i++)
        {
            ammoImages[i].SetActive(true);
        }
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public float GetWeaponOffset()
    {
        return weaponOffset;
    }

    public void Reload()
    {
        ResetReloadText();
        ReloadAmmoPanel(currMag);
        currMag = magSize;
    }

    private void ResetReloadText()
    {
        if (bullets != -1)
            reloadText.text = bullets.ToString();
        else
            reloadText.text = "Inf";
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

    public bool Fire()
    {
        if (currMag <= 0)
        {
            reloadText.text = "RELOAD";
            return false;
        }
        else
        {
            ammoImages[currMag - 1].SetActive(false);
            currMag--;
            if (bullets != -1)
            {
                bullets--;
                reloadText.text = bullets.ToString();
            }
            GameObject shot = Instantiate(shotPrefab, transform) as GameObject;
            fireFx.Play();
            shot.transform.rotation = transform.rotation;
            shot.transform.localPosition += new Vector3(weaponOffset, 0f);
            // TODO: add relative velocity of player for consistant projectile speed
            shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(fireSpeed, 0.0f));
            shot.transform.SetParent(transform.parent.parent);
            // set parent as world
            if (bullets == 0)
                DiscardWeapon();
            return true;
        }
    }

    private void DiscardWeapon()
    {
        for (int i = 0; i < magSize; i++)
        {
            Destroy(ammoImages[i]);
        }
        Destroy(gameObject);
    }
}
