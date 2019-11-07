﻿using UnityEngine;

public class EnemyArcher : Enemy{
    [Header("Archer")]
    [SerializeField] private float minDistanceToMoveBack;

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

        if (isMyAttackingTurn && timeLeft <= 0.0f && IsOnAttackRange()){
            enemyMovement.IsMovingForward = false;
            enemyCombat.Attack();
            isMyAttackingTurn = false;
            isInvokingAttack = false;
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