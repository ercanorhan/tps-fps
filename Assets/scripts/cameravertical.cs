using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameravertical : MonoBehaviour
{
    public float rotationSpeed;
    public float yMoveThreshold = 1000.0f;
    private float yMinLimit = -28f;
    private float yMaxLimit = 28f;
    float yRotCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        //transform.Rotate(Vector3.left * mouseY * rotationSpeed);
        yRotCounter += Input.GetAxis("Mouse Y") * yMoveThreshold * Time.deltaTime;
        yRotCounter = Mathf.Clamp(yRotCounter, yMinLimit, yMaxLimit);
        transform.localEulerAngles = new Vector3(-yRotCounter, 0, 0);
    }
}
