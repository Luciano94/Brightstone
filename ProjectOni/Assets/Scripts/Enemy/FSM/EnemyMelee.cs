using UnityEngine;

public class EnemyMelee : Enemy{
    [Header("OwnVariables")]
    [SerializeField] private float maxDistSurround;

    protected override void Chasing(){
        if (IsOnAttackRange()){
            enemyCombat.Attack();
            OnAttackRange();
            return;
        }

        enemyMovement.MoveToPlayer();
    }
    
    protected override void Waiting(){
        if (IsOnChaseRange()){
            OnChase();
            return;
        }
        
        if (enemyMovement.DistToPlayer() > maxDistSurround)
            enemyMovement.MoveToPlayer();
        else
            enemyMovement.SurroundPlayer();
    }

    protected override void Relocating(){
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f){
            enemyMovement.IsMovingForward = false;
            OnChase();
            return;
        }
        
        enemyMovement.Relocate();
    }

    protected override void Attacking(){
        if (!enemyCombat.IsAttacking){
            timeLeft = timeRelocating;
            OnRelocate();
            return;
        }

        enemyCombat.Attacking();
    }

    protected override void Hurt(){
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f){
            enemyCombat.Restitute();
            OnRestitution();
            return;
        }

        enemyMovement.MoveByHit();
    }
}