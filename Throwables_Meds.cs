using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables_Meds : MonoBehaviour
{
    private GameObject ScriptHolder;
    [HideInInspector] public float MedPacksAmount = 1;
    [HideInInspector] public float GrenadesAmount = 1;
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && ScriptHolder.CompareTag("Med")){
            MedPacksAmount += 1;
        }

        if(other.CompareTag("Player") && ScriptHolder.CompareTag("Grenade")){
            GrenadesAmount += 1;
        }
    }
}