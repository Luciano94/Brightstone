using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    PlayerStats playerStats;
    EnemyStats enemyStats;

    private void Start() {
        playerStats = GameManager.Instance.playerSts;
       // enemyStats = GameManager.Instance.enemySts;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 14 &&
            GameManager.Instance.PlayerIsAttack){
            enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            enemyStats.Life = playerStats.AtkDmg;
        }
    }
}