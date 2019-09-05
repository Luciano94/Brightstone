using UnityEngine;

public class ArrowTrigger : MonoBehaviour{
    public EnemyStats enemyStats;

    private void OnTriggerEnter2D(Collider2D collision){
        GameManager gM = GameManager.Instance;

        switch(collision.tag){
            case "Player":
                if(!gM.PlayerIsParry){
                    enemyStats.Hit();
                    gM.playerSts.Life = enemyStats.AtkDmg;
                    gM.SetEnemyHitFrom(transform.position);
                    gM.ShakerController.Shake(1.2f, 1.2f, 0.1f, 0.2f);
                    UIManager.Instance.LifeUpdate();
                }else{
                    AudioManager.Instance.PlayerParry();
                    gM.playerCombat.ParriedSomeone();
                    gM.ZoomWhenParrying.ReduceSize();
                }
                Destroy(transform.parent.gameObject);
            break;

            case "Wall":
                Destroy(transform.parent.gameObject);
            break;
        }
    }
}
