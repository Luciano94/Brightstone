using UnityEngine;

public class EnemyWeapon : MonoBehaviour{
    [SerializeField] EnemyStats enemyStats;
    [SerializeField] EnemyCombat enemyCombat;

    private GameManager gM;

    private void Start(){
        gM = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            if(!gM.PlayerIsParry){
                enemyStats.Hit();

                switch(enemyStats.enemyType){
                    case EnemyType.Boss:
                        SoundManager.Instance.BossSwordAttackHit(gameObject);
                    break;
                    default:
                        SoundManager.Instance.EnemyMeleeSwordAttackHit(gameObject);
                    break;
                }

                gM.playerSts.Life = enemyStats.AtkDmg;
                gM.SetEnemyHitFrom(transform.position);
                gM.ShakerController.Shake(1.2f, 1.2f, 0.1f, 0.2f);
                UIManager.Instance.LifeUpdate();
            }else{
                //AudioManager.Instance.PlayerParry();
                enemyStats.Parried();
                gM.playerCombat.ParriedSomeone();
                gM.ZoomWhenParrying.ReduceSize();
            }
        }
    }
}
