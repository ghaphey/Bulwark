using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] Texture2D cursorTex;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTex, hotSpot, cursorMode);
        weapon = GameObject.FindGameObjectWithTag("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateWeaponDirection();
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


    }
}
