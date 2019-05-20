using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    //[SerializeField] private GameObject shotPrefab = null;
    //[SerializeField] private float weaponOffset = 0.7f;
    //[SerializeField] private float fireSpeed = 500f;

    [SerializeField] private Texture2D cursorTex = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private Transform held;
    private Weapon weapon;
    private float reloadTimer = 0f;
    private bool hasAmmo = true;
    private Animator anim = null;

    void Start()
    {
        Cursor.SetCursor(cursorTex, hotSpot, cursorMode);
        held = GameObject.FindGameObjectWithTag("Gun").transform;
        weapon = GetComponentInChildren<Weapon>();
        weapon.transform.localPosition = new Vector3(weapon.GetWeaponOffset(), 0f);
        anim = GetComponent<Animator>();
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
        if (hit)
        {
            // TODO: Messy, must be a better solution. Also buggy

            Quaternion rotation = Quaternion.LookRotation(hit.point - (Vector2)held.position, held.TransformDirection(Vector3.up));
            held.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            float zAng = held.rotation.eulerAngles.z;
            if (zAng <= 90f || zAng >= 270f)
            {
                weapon.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                weapon.transform.localScale = new Vector3(1, -1, 1);
            }
        }

    }

    private void FireControl()
    {
        if (reloadTimer <= 0f)
        {
            if (Input.GetButtonDown("Fire1") && hasAmmo)
            {
                hasAmmo = weapon.Fire();
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                reloadTimer = weapon.GetReloadTime();
            }
        }
        else
        {
            //TODO: RELOAD ANIMATIONS
            reloadTimer -= Time.deltaTime;
            weapon.AdjustReloadBar(reloadTimer);
            if (reloadTimer <= 0f)
            {
                weapon.Reload();
                hasAmmo = true;
            }
        }
    }

}

/*
RaycastHit hit;
Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            turretBase.GetChild(0).LookAt(hit.point);
        }

RaycastHit hit;
if (Physics.Raycast(barrel.position, barrel.TransformDirection(Vector3.up), out hit))
{
    Instantiate(hitFX, hit.point, Quaternion.identity);
   if (hit.transform.GetComponent<EnemyHealth>())
      
    hit.transform.GetComponent<EnemyHealth>().ApplyDamage(cannonDamage);
}
*/

