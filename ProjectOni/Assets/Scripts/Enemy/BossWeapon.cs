using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour {
    PlayerStats playerStats;
    [SerializeField] BossStats bossStats;
    GameManager gameM;
    private void Start() {
        playerStats = GameManager.Instance.playerSts;
        gameM = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!gameM.PlayerIsParry){
            bossStats.Hit();
            gameObject.SetActive(false);
            playerStats.Life = bossStats.AtkDmg;
            UIManager.Instance.lifeUpdate();
        }
    }
}
