using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    PlayerStats playerStats;

    private void Awake() {
        playerStats = GameManager.Instance.playerSts;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        playerStats.Life = 10.0f;
        UIManager.Instance.lifeUpdate();
    }
}
