using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static IWeaponStats;

public class WeaponManager : MonoBehaviour
{
    int previousWeaponIndex = -1;
    bool IsWeaponActivated;
    public List<GameObject> weapons = new List<GameObject>();
    public static int weaponIndex = 0;
    private Transform currentWeapon;
    public LayerMask enemyLayer;


    private void Start() {
        Select();
    }

    private void Update() {
        WeaponChange();
    }


    void WeaponChange(){
        
        int previousWeapon = weaponIndex;

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            weaponIndex = 0;
            Debug.Log(weaponIndex);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2){
            weaponIndex = 1;
            Debug.Log(weaponIndex);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3){
            weaponIndex = 2;
            Debug.Log(weaponIndex);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4){
            weaponIndex = 3;
            Debug.Log(weaponIndex);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5){
            weaponIndex = 4;
            Debug.Log(weaponIndex);
        }

        if(previousWeapon != weaponIndex){
            Select();
        }
    }

    public void Select(){
        int i = 0;
        foreach(Transform weapon in transform){
            if(i == weaponIndex){
                weapon.gameObject.SetActive(true);
            }else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}