using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    PlayerStats playerStats;
    [SerializeField]EnemyStats enemyStats;
    GameManager gameM;
    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
        gameM = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!gameM.PlayerIsParry){
            playerStats.Life = enemyStats.AtkDmg;
            UIManager.Instance.lifeUpdate();
        }
    }
}
