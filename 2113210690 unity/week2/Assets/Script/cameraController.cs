using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField] private Transform target;

    [SerializeField] private float distanceFromTarget = 3.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2 (-40, 40);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        // apply clamping for x rotation
        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        //apply damping between rotaton changes
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        // subtract forward vector of the gameobject to point its forward vector to the target
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
