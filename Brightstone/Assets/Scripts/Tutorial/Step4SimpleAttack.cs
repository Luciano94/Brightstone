using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step4SimpleAttack : Step{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private float minDistance;
    private PlayerCombat playerCombat;

    bool usedSimpleAttack = false;
    bool usedStrongAttack = false;

    public override void StepInitialize(){
        playerCombat = GameManager.Instance.playerCombat;
        playerCombat.enabled = false;
        enemyStats.OnHit.AddListener(EnemyHit);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        Vector3 diff = GameManager.Instance.PlayerPos - enemyStats.transform.position;
        diff.z = 0.0f;
        float dist = diff.magnitude;

        if (dist <= minDistance){
            playerCombat.enabled = true;
        }
        else{
            playerCombat.enabled = false;
        }
    }

    private void EnemyHit(){
        if (enemyStats.LastDamageRecieved() == 15.0f){
            if (!usedSimpleAttack){
                
            }
            
            usedSimpleAttack = true;
        }

        if (enemyStats.LastDamageRecieved() == 40.0f){
            if (!usedStrongAttack){

            }
            
            usedStrongAttack = true;
        }

        enemyStats.Life = -enemyStats.LastDamageRecieved();
    }
}
