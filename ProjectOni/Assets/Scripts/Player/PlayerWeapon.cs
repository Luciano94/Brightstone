using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour{
    PlayerStats playerStats;
    const int ENEMY_LAYER = 14;

    private void Start(){
        playerStats = GameManager.Instance.playerSts;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == ENEMY_LAYER &&
            GameManager.Instance.PlayerIsAttack){
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            enemyStats.Life = playerStats.AtkDmg * playerStats.AtkMult;
        }
    }
}