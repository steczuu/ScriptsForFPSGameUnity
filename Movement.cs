using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    Vector3 destination,playerVelocity;
    CharacterController controller;
    private float speed = 10f,x,z,jumpForceStrength = 1f,gravity = -9.81f;
    public GameObject Camera,animObj,AR;
    private bool isOnGround;
    public static bool IsSprinting, CanMove;

    private void Start() {
        controller = GetComponent<CharacterController>();
        CanMove = true;
    }

    void Update() {
        isOnGround = controller.isGrounded;

        Vector3 movement = transform.right * -x + transform.forward * -z;

        controller.Move(movement * speed * Time.deltaTime);
        if(CanMove){
            Move();
        }
    }

    void Move(){
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && WeaponManager.weaponIndex == 1){
            speed = 15f;
            IsSprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && WeaponManager.weaponIndex == 1){
            speed = 10f;
            IsSprinting = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && WeaponManager.weaponIndex == 3){
            speed = 15f;
            IsSprinting = true;
        }

        if(isOnGround && Input.GetKey(KeyCode.Space)){
            playerVelocity.y += Mathf.Sqrt(jumpForceStrength * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}