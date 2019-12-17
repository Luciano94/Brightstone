using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloUser : MonoBehaviour{
    private PlayerCombat playerCombat;
    private Component halo;
    private bool haloVisible = false;
    private float timer = 0.25f;
    private float timeLeft;

    private void Awake(){
        playerCombat = GetComponent<PlayerCombat>();
        halo = GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);

        playerCombat.OnParriedSomeone().AddListener(OnParried);
    }

    private void Update(){
        if (haloVisible){
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0.0f){
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                haloVisible = false;
            }
        }
    }

    private void OnParried(){
        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
        haloVisible = true;
        timeLeft = timer;
    }
}
