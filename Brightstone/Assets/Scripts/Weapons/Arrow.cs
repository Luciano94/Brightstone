using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour{
    [SerializeField] private float speed;
    private EnemyStats enemyStats;

    private void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetEnemyStats(EnemyStats enemyStats){
        this.enemyStats = enemyStats;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameManager gM = GameManager.Instance;

        if (collision.tag == "Player"){
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
        }

        Destroy(gameObject);
    }
}
