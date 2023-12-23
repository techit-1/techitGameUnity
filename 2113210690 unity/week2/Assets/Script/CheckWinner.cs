using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public static CheckWinner instance;
    
    public Camera defaultCamera;
    public Camera winnerCamera;
    public bool isWinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public Transform playerRotation;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner) 
        {
            //winer is dtermined switch to winner camera
            defaultCamera.enabled = false;
            winnerCamera.enabled = true;
        }
        
    }

    private void LateUpdate()
    {
        if (target != null && isWinner)
        {
            //calculated desired position for camera
            Vector3 desiredPosition = new Vector3(target.position.x + 2.5f, target.position.y + 0.5f, target.position.z);

            //smoothly move camera to desired position
            Vector3 smoothedPosition = Vector3.Lerp(winnerCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            winnerCamera.transform.position = smoothedPosition;

            playerRotation.LookAt(new Vector3(winnerCamera.transform.position.x, playerRotation.position.y, playerRotation.position.z));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.groundedPlayer)
        {
            isWinner = true;
        }

    }
}
