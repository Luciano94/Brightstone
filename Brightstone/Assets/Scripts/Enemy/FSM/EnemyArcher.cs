using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy{
    [Header("OwnVariables")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private float distFromOrigin;
    [SerializeField] private float minDistanceToMoveBack;

    protected override void Awake(){
        base.Awake();
        enemyCombat.HasThrowableObject(arrow, distFromOrigin);

        isMyAttackingTurn = true;
    }

    protected override void Chasing(){
        timeLeft = timeRelocating;
        OnForceToRelocate();
    }
    
    protected override void Waiting(){
        timeLeft = timeRelocating;
        OnForceToRelocate();
    }

    protected override void Relocating(){
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f){
            enemyMovement.IsMovingForward = false;
            enemyCombat.Attack(GetEnemyType());
            OnAttackRange();
            return;
        }
        
        enemyMovement.RelocateArcher(minDistanceToMoveBack);
    }

    protected override void Attacking(){
        if (!enemyCombat.IsAttacking){
            timeLeft = timeRelocating;
            enemyMovement.RandomizeDirection();
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