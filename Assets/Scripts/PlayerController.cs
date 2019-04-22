﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject shotPrefab = null;
    [SerializeField] private float weaponOffset = 0.7f;

    [SerializeField] Texture2D cursorTex = null;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private Transform weapon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTex, hotSpot, cursorMode);
        weapon = GameObject.FindGameObjectWithTag("Gun").transform;
    }

    // Update is called once per frame
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

        Vector2 newPos = new Vector2(transform.position.x + nXPos, transform.position.y + nYPos);
        transform.position = newPos;
    }

    private void UpdateWeaponDirection()
    {
        Vector2 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit)
        {
            //print(hit.point);
            // TODO: Messy, must be a better solution. Also buggy
            // TODO: mirror gun when it goes to other axis
            Quaternion rotation = Quaternion.LookRotation(hit.point - (Vector2)weapon.position, weapon.TransformDirection(Vector3.up));
            weapon.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        }

    }

    private void FireControl()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject shot = Instantiate(shotPrefab, weapon.transform) as GameObject;
            shot.transform.rotation = weapon.rotation;
            shot.transform.localPosition += new Vector3(weaponOffset, 0f);
            shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(shot.GetComponent<DamageDealer>().GetSpeed(), 0.0f));
            shot.transform.SetParent(transform.parent);
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

