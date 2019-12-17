using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCombat : EnemyCombat{
    override public void Attack(){
        base.Attack();
    }

    override public void Attacking(){
        base.Attacking();

        if(currentTime > activeMoment && !active){
            weapon.SetActive(true);
            weaponColl.enabled = true;
            //AudioManager.Instance.EnemyAttack();
            SoundManager.Instance.EnemyMeleeSwordAttack(gameObject);
            active = true;
        }
        if(currentTime > animTime){
            weapon.SetActive(false);
            weaponColl.enabled = false;
            isAttacking = false;
            currentTime = 0.0f;
            active = false;
        }
    }
}
