using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    PlayerStats playerStats;
    [SerializeField]EnemyStats enemyStats;
    [SerializeField]EnemyCombat enemyCombat;
    GameManager gameM;
    private void Start() {
        playerStats = GameManager.Instance.playerSts;
        gameM = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!gameM.PlayerIsParry){
            enemyStats.Hit();
            playerStats.Life = enemyStats.AtkDmg;
            UIManager.Instance.lifeUpdate();
            enemyCombat.EndAttack();
        }else{
            enemyCombat.EndAttack();            
        }
    }
}
