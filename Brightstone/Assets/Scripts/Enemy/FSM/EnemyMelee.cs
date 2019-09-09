using UnityEngine;

public class EnemyMelee : Enemy{
    [Header("OwnVariables")]
    [SerializeField] private float maxDistSurround;

    public int chaserIndex;

    protected override void Chasing(){
        if (isMyAttackingTurn){
            if (IsOnAttackRange()){
                isMyAttackingTurn = false;
                enemyCombat.Attack(GetEnemyType());
                OnAttackRange();
                return;
            }

            enemyMovement.MoveToPlayer();
        }
        else{
            enemyMovement.ApplyMovementStrategy(chaserIndex);
        }
    }
    
    protected override void Waiting(){
        if (IsOnChaseRange()){
            EnemyBahaviour.Instance.WarriorAddedToChase(gameObject);
            OnChase();
            return;
        }
        
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

        enemyCombat.Attacking(GetEnemyType());
    }

    protected override void Hurt(){
        timeLeftHit -= Time.deltaTime;
        timeLeftParried -= Time.deltaTime;
        if (timeLeftHit <= 0.0f && timeLeftParried <= 0.0f){
            enemyCombat.Restitute();
            OnRestitution();
            return;
        }

        if (timeLeftHit >= 0.0f)
            enemyMovement.MoveByHit();
        else
            enemyMovement.MoveByParried();
    }
}