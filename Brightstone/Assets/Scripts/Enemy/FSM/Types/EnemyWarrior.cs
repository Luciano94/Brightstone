using UnityEngine;

public class EnemyWarrior : Enemy{
    [Header("Warrior")]
    [SerializeField] private float maxDistSurround;

    protected override void Chasing(){
        if (isMyAttackingTurn){
            if (IsOnAttackRange()){
                if(feinting){
                    feinting = false;
                    isMyAttackingTurn = false;
                    return;
                }
                isMyAttackingTurn = false;
                enemyCombat.Attack();
                OnAttackRange();
                return;
            }

            enemyMovement.MoveToPlayer();
        }
        else{
            enemyMovement.ApplyMovementStrategy(enemyIndex);
        }
    }
    
    protected override void Waiting(){
        /*if (IsOnChaseRange()){
            EnemyBehaviour.Instance.WarriorAddedToChase(gameObject);
            OnChase();
            return;
        }*/
        
        enemyMovement.SurroundPlayer();
    }

    protected override void Relocating(){
        OnChase();
        
        /*timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f){
            enemyMovement.IsMovingForward = false;
            OnChase();
            return;
        }
        
        enemyMovement.Relocate();*/
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