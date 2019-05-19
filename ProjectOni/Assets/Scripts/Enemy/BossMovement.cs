using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Vector3 player;
    [SerializeField] private GameObject sword;
    [SerializeField]private EnemyAnimations eAnim;
    private Vector3 diff;
    private Vector3 dir;

    private BossCombat bossCombat;

    private void Start() {
        player = GameManager.Instance.PlayerPos;
        bossCombat = GetComponent<BossCombat>();
    }

    private void OnEnable() {
        UIManager.Instance.InitBoss();
    }

    private void Update() {
        player = GameManager.Instance.PlayerPos;
        
        if(bossCombat.IsHit) {
            MoveByHit();
            return;
        }
        if(!bossCombat.IsAttacking) {
            Movement();
            Rotation();
        }
    }

    private void Movement() {
        diff = player - transform.position;
        float dist = diff.magnitude;
        if(player.x > transform.position.x){
            eAnim.SetDirection(0);
        }else{
            eAnim.SetDirection(1);
        }
        if(dist > 1.5f)
            transform.Translate(diff.normalized * speed * Time.deltaTime); 
    }

    private void Rotation() {
        diff = player - transform.position;
        dir = diff.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    private void MoveByHit() {
        transform.Translate(-diff.normalized * speed * 0.3f * Time.deltaTime);
    }
}
