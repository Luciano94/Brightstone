using UnityEngine;

public class EnemyAnimations : MonoBehaviour{

    [SerializeField] private Animator anim;
    
    public void SetDirection(int dir){
        anim.SetInteger("Direction", dir);
    }

    public void SetAttack(){
        anim.SetTrigger("Attack");
    }

    public void MovingBackwards(){
        anim.SetFloat("ForBackwards", 1.0f);
    }

    public void StopMovingBackwards(){
        anim.SetFloat("ForBackwards", -1.0f);
    }
}
