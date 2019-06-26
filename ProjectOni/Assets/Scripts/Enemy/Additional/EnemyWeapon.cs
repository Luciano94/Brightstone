using UnityEngine;

public class EnemyWeapon : MonoBehaviour{
    PlayerStats playerStats;
    [SerializeField]EnemyStats enemyStats;
    [SerializeField]EnemyCombat enemyCombat;
    GameManager gameM;
    private void Start(){
        playerStats = GameManager.Instance.playerSts;
        gameM = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!gameM.PlayerIsParry){
            enemyStats.Hit();
            playerStats.Life = enemyStats.AtkDmg;
            GameManager.Instance.SetEnemyHitFrom(transform.position);
            UIManager.Instance.LifeUpdate();
        }else{
            AudioManager.Instance.PlayerParry();
            enemyStats.Parried();
            gameM.playerCombat.ParriedSomeone();
        }
    }
}
