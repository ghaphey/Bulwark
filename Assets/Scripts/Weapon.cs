using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header ("Weapon Properties")]
    [SerializeField] private float fireSpeed = 500f;
    [SerializeField] private int magSize = 10;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private float weaponOffset = 0.7f;
    [SerializeField] private GameObject shotPrefab = null;
    [Header("UI Properties")]
    [SerializeField] private RectTransform ammoPanel = null;
    [SerializeField] private GameObject ammoImage = null;
    [SerializeField] private float widthOffset = 40f;
    //[SerializeField] private float heightOffset = 60f;

    private int currMag = 0;
    private List<GameObject> ammoImages = null;
    private Text reloadText = null;
    private float reloadBarWidth = 0f;
    private Slider reloadBar = null;

    void Start()
    {
        transform.localPosition = new Vector3(weaponOffset, 0f);
        reloadText = ammoPanel.GetComponentInChildren<Text>();
        reloadText.enabled = false;
        reloadBarWidth = ammoPanel.rect.width;
        reloadBar = ammoPanel.GetComponentInChildren<Slider>();
        reloadBar.gameObject.SetActive(false);
        ammoImages = new List<GameObject>();
        PopulateAmmoPanel(currMag);
        currMag = magSize;
    }

    private void PopulateAmmoPanel(int currIndex)
    {
        for (int i = currIndex; i < magSize; i++)
        {
            ammoImages.Add(Instantiate(ammoImage, ammoPanel));
            RectTransform nAmmo = ammoImages[i].GetComponent<RectTransform>();
            nAmmo.Translate(new Vector3(widthOffset * -i * 1.25f, 0f));
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
        reloadText.enabled = false;
        reloadBar.gameObject.SetActive(false);
        ReloadAmmoPanel(currMag);
        currMag = magSize;
    }

    public void AdjustReloadBar(float currTimer)
    {
        reloadBar.gameObject.SetActive(true);
        reloadBar.value = currTimer / reloadTime;
    }

    public bool Fire()
    {
        if (currMag <= 0)
        {
            reloadText.enabled = true;
            return false;
        }
        else
        {
            ammoImages[currMag - 1].SetActive(false);
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
