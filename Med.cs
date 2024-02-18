using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Med : MonoBehaviour
{
    public float HP_Added = 40;
    private float cooldown = 1f,AnimationTimer;
    bool hasHealed = false;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.V) && !hasHealed){
            hasHealed = true;
            //Play animation
            StartCoroutine(Heal());
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Heal(){
        yield return new WaitForSeconds(AnimationTimer);
        PlayerManger.PlayerHp += HP_Added;
    }

    IEnumerator Cooldown(){
        hasHealed = true;
        yield return new WaitForSeconds(cooldown);
        hasHealed = false;
    }
}