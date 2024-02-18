using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPacks : MonoBehaviour
{
    public float BulletsInPack;


    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
           Shooting.ammoTotal += BulletsInPack;
           Destroy(gameObject);
        }
    }
}