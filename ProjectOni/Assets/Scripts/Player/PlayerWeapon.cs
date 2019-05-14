using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    PlayerStats playerStats;
    //EnemyStats enemyStats;
    //BossStats bossStats;

    private void Start() {
        playerStats = GameManager.Instance.playerSts;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 14 &&
            GameManager.Instance.PlayerIsAttack){
            
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            if (enemyStats)
                enemyStats.Life = playerStats.AtkDmg;
            else {
                BossStats bossStats = collision.gameObject.GetComponent<BossStats>();
                bossStats.Life = playerStats.AtkDmg;
            }
        }
    }
}