using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private void Start() {
        PlayerManger.PlayerHp = 100f;
        Shooting.ammoTotal = Shooting.ammoReserve;
        Shooting.Ammo = Shooting.MaxAmmo;
    }

    public static void ResetScene(){
        SceneManager.LoadScene(0);
    }
}
