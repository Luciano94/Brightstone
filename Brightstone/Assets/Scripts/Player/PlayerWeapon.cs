using UnityEngine;

public class PlayerWeapon : MonoBehaviour{
    PlayerStats playerStats;
    PlayerCombat playerCombat;
    const int ENEMY_LAYER = 14;

    private void Start(){
        playerStats = GameManager.Instance.playerSts;
        playerCombat = GameManager.Instance.playerCombat;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (GameManager.Instance.PlayerIsAttack){
            switch(collision.tag){
                case "Enemy":
                    EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
                    enemyStats.Life = playerStats.AtkDmg * playerStats.AtkMult * GameManager.Instance.comboMult;
                    GameManager.Instance.ShakerController.Shake(1.6f, 1.6f, 0.1f, 0.24f);
                    SoundManager.Instance.PlayerAttackLightHit();
                break;

                case "Arrow":
                    Destroy(collision.gameObject);
                break;
            }
        }
    }
}