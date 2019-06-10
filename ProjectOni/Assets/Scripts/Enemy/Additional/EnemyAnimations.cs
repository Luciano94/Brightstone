using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{

    [SerializeField]private Animator anim;

    public void SetDirection(int dir){
        anim.SetInteger("Direction", dir);
    }

    public void SetAttack(){
        anim.SetTrigger("Attack");
    }
}
