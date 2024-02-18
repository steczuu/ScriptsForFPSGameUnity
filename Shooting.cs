using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class Shooting : MonoBehaviour
{   
    public RecoilLogic recoil;
    public GameObject gun,AR,crosshair,barrel,hitMarker;
    AudioSource audioSource;
    public Camera cam;
    public float ReloadDuration = 2f;
    private float usedBullets, AimFOV = 50, speedFOV = 200f, time = 2f;
    public static float X, Y, damage,ammoTotal,Ammo,shootingRange,ammoReserve,EffectiveRange,MaxAmmo,cooldownDuration;
    public static bool canShoot = true, hasShot;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        hitMarker.SetActive(false);
        hasShot = false;
    }

    private void Update(){

        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5)){
                ChangeStats();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1)){
            Aim();
            crosshair.SetActive(false);
            gun.GetComponent<Animator>().Play("Aim");
            AR.GetComponent<Animator>().Play("AimAR");
        }

        if(Input.GetKeyUp(KeyCode.Mouse1)){
            cam.fieldOfView = 70;
            crosshair.SetActive(true);
            gun.GetComponent<Animator>().Play("UnAim");
            AR.GetComponent<Animator>().Play("UnAimAR");
        }
        
        if(Input.GetKeyDown(KeyCode.R) && Ammo < MaxAmmo && ammoTotal > 0){
            Reload();
        }


        if(Input.GetKey(KeyCode.Mouse0) && Ammo > 0 && canShoot && !Movement.IsSprinting){
            Shoot();
            Sound();
        }
    }

    public Shooting(IStatistics statistics){
        _statistics = statistics;
    }

    void Aim(){
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, AimFOV, speedFOV * Time.deltaTime);
    }

    void Shoot(){
        Ammo -= 1;
        usedBullets += 1;
        hasShot = true;
        StartCoroutine(ShootCooldown());

        recoil.Recoil();
        RaycastHit raycastHit;
        if(Physics.Raycast(barrel.transform.position, barrel.transform.forward, out raycastHit , shootingRange)){
            TestAi testAi = raycastHit.transform.GetComponent<TestAi>();
            HitSystem hitSystem = raycastHit.transform.GetComponent<HitSystem>();

            Debug.Log(raycastHit.transform.name);

            if(testAi != null){
                testAi.UpdateHealth(damage);
                hitMarker.SetActive(true);

                StartCoroutine(HitMarkerTimer());
            }
            
            if(hitSystem != null){
                hitSystem.UpdateStatus(damage);
            }
            
            else{
                hitMarker.SetActive(false);
            }
        }
    }

    void Reload(){
        Ammo = MaxAmmo;
        ammoTotal -= usedBullets;
         
        StartCoroutine(ReloadTime());
    }

    IEnumerator HitMarkerTimer(){
        yield return new WaitForSeconds(0.5f);
        hitMarker.SetActive(false);
    }

    IEnumerator ReloadTime()
    {
        canShoot = false;
        yield return new WaitForSeconds(ReloadDuration);
        usedBullets = 0f;
        Debug.Log(usedBullets);
        canShoot = true;
    }

    IEnumerator ShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(cooldownDuration);
        canShoot = true;
    }

    void Sound(){
        audioSource.Play();
    }

     void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;

        Vector3 direction = transform.TransformDirection(Vector3.forward) * shootingRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}