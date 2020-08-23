using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    //[SerializeField] private GameObject shotPrefab = null;
    //[SerializeField] private float weaponOffset = 0.7f;
    //[SerializeField] private float fireSpeed = 500f;

    [SerializeField] private Texture2D cursorTex = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Transform pivot = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    
    private List<Weapon> weapons = new List<Weapon>();
    private int weaponIndex = 0;
    private float reloadTimer = 0f;
    private int hasAmmo = 0;
    private Animator anim = null;
    private AmmoUI ammoUI = null;

    //private Slider ammoPanel = null;

    void Start()
    {
        Cursor.SetCursor(cursorTex, hotSpot, cursorMode);
        weapons.Add(pivot.GetComponentInChildren<Weapon>());
        weapons[weaponIndex].transform.localPosition = new Vector3(weapons[weaponIndex].GetWeaponOffset(), 0f);
        //weapons[weaponIndex].ActivateAmmoPanel();

        anim = GetComponent<Animator>();
        hasAmmo = weapons[weaponIndex].GetCurrMag();
        ammoUI = GameObject.FindGameObjectWithTag("Ammo").GetComponentInChildren<AmmoUI>();
        ammoUI.ResetAmmoPanel(weapons[weaponIndex].GetAmmoImage(),
                              weapons[weaponIndex].GetMagSize(),
                              weapons[weaponIndex].GetWidthOffset());
    }

    void Update()
    {
        UpdateMovement();
        UpdateWeaponDirection();
        FireControl();
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float nXPos = horizontal * speed * Time.deltaTime;
        float nYPos = vertical * speed * Time.deltaTime;

        anim.SetBool("moving", true);
        if (nXPos < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (nXPos > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            anim.SetBool("moving", false);
        }

            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + nXPos, transform.position.y + nYPos));
    }

    private void UpdateWeaponDirection()
    {
        Vector2 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit && weapons[weaponIndex] != null)
        {
            // TODO: Messy, must be a better solution. Also buggy

            Quaternion rotation = Quaternion.LookRotation(hit.point - (Vector2)pivot.position, pivot.TransformDirection(Vector3.up));
            pivot.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            float zAng = pivot.rotation.eulerAngles.z;
            if (zAng <= 90f || zAng >= 270f)
            {
                weapons[weaponIndex].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                weapons[weaponIndex].transform.localScale = new Vector3(1, -1, 1);
            }
        }

    }

    private void FireControl()
    {
        if (reloadTimer <= 0f)
        {
            if (Input.GetButtonDown("Fire1") && hasAmmo > 0)
            {
                hasAmmo = weapons[weaponIndex].Fire();
                ammoUI.UsedAmmo(hasAmmo);
                if(hasAmmo == -1)
                {
                    weapons.RemoveAt(weaponIndex);
                    weaponIndex = 0;
                    weapons[weaponIndex].gameObject.SetActive(true);
                    ammoUI.ResetAmmoPanel(weapons[weaponIndex].GetAmmoImage(),
                                                  weapons[weaponIndex].GetMagSize(),
                                                  weapons[weaponIndex].GetWidthOffset());
                    hasAmmo = weapons[weaponIndex].GetCurrMag();
                    SetAmmoCount();
                }
                else if ( hasAmmo <= 0)
                    ammoUI.SetText("RELOAD");
                else
                {
                    SetAmmoCount();
                }
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                ammoUI.SetText("Reloading...");
                reloadTimer = weapons[weaponIndex].GetReloadTime();
            }
        }
        else
        {
            //TODO: RELOAD ANIMATIONS
            reloadTimer -= Time.deltaTime;
            ammoUI.AdjustReloadSlider(weapons[weaponIndex].AdjustReloadBar(reloadTimer));
            if (reloadTimer <= 0f)
            {
                SetAmmoCount();
                weapons[weaponIndex].Reload();
                hasAmmo = weapons[weaponIndex].GetCurrMag();
                ammoUI.ReloadAmmo(hasAmmo);
            }
        }
    }

    private void SetAmmoCount()
    {
        int count = weapons[weaponIndex].GetAmmunitionCount();
        if (count != -1)
            ammoUI.SetText(count.ToString());
        else
            ammoUI.SetText("Inf");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gun")
        {
            if (weapons.Count > 1)
            {
                weapons[1].AddAmmunition(collision.GetComponent<Weapon>().GetAmmunitionCount());
                Destroy(collision.gameObject);
            }
            else
            {
                Weapon temp = collision.GetComponent<Weapon>();
                if (temp != null)
                {
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    collision.gameObject.transform.parent = pivot;
                    collision.gameObject.transform.localPosition = new Vector3(weapons[weaponIndex].GetWeaponOffset(), 0f, 0f);
                    collision.gameObject.transform.localRotation = Quaternion.identity;
                    weapons.Add(temp);
                    weapons[weaponIndex].gameObject.SetActive(false);
                    weaponIndex++;
                    weapons[weaponIndex].gameObject.SetActive(true);
                    hasAmmo = weapons[weaponIndex].GetCurrMag();
                    ammoUI.ResetAmmoPanel(weapons[weaponIndex].GetAmmoImage(),
                              hasAmmo,
                              weapons[weaponIndex].GetWidthOffset());
                    SetAmmoCount();
                }
            }
        }
    }

}


