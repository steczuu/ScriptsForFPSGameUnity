using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSens = 850f;
    public Transform player;
    private float xAxisRotation,x,y;

   
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        x = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        y = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xAxisRotation -= y;
        xAxisRotation = Mathf.Clamp(xAxisRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xAxisRotation, 0 ,0);
        player.Rotate(Vector3.up * x);
    }
}
