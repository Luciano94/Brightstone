using UnityEngine;

public class PlayerWeapon : MonoBehaviour{
    PlayerStats playerStats;
    const int ENEMY_LAYER = 14;

    private void Start(){
        playerStats = GameManager.Instance.playerSts;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (GameManager.Instance.PlayerIsAttack){
            switch(collision.tag){
                case "Enemy":
                    EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
                    enemyStats.Life = playerStats.AtkDmg * playerStats.AtkMult;
                    GameManager.Instance.ShakerController.Shake();
                break;

                case "Arrow":
                    Destroy(collision.gameObject);
                break;
            }
        }
    }
}