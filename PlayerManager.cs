using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManger : MonoBehaviour
{
    public bool isAlive;
    public static float PlayerHp = 100f;
    public TMP_Text hpText;
    public GameObject panel;

    private void Start() {
        isAlive = true;
        hpText.text = "";
    }

    private void Update() {
        hpText.text = PlayerHp.ToString();

        if(PlayerHp <= 30f){
            hpText.color = new Color(255, 0, 0);
            panel.SetActive(true);
        }
    }

    public void UpdateHealth(float damage){
        PlayerHp -= damage;
        if(PlayerHp <= 0){
            isAlive = false;
            SceneManagement.ResetScene();
        }
    }

    public void Stun(){
        StartCoroutine(StunTime());

        Shooting.canShoot = false;
        Movement.CanMove = false;
    }

    IEnumerator StunTime(){
        yield return new WaitForSeconds(1.5f);
        Shooting.canShoot = true;
        Movement.CanMove = true;        
    }
}
