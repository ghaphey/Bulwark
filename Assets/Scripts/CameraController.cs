using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float fieldWidth = 33f;
    [SerializeField] private float cameraFOV = 8.5f;

    [SerializeField] Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.x < 0 && playerPos.position.x > -fieldWidth + cameraFOV * 2)
        {
            transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
        }
    }
}
