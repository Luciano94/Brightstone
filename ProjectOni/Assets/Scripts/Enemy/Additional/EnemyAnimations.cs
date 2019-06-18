using UnityEngine;

public class EnemyAnimations : MonoBehaviour{

    [SerializeField] private Animator anim;
    private float speed = 1.0f;
    
    public void SetDirection(int dir){
        anim.SetInteger("Direction", dir);
    }

    public void SetAttack(){
        anim.SetTrigger("Attack");
    }

    public void MovingBackwards(){
        anim.SetFloat("AnimSpeed", speed * 1.0f);
    }

    public void StopMovingBackwards(){
        anim.SetFloat("AnimSpeed", speed * -1.0f);
    }

    public void UpdateSpeed(float speed){
        float sign = Mathf.Sign(anim.GetFloat("AnimSpeed"));
        this.speed = speed * sign;
        anim.SetFloat("AnimSpeed", speed);
    }

    public void Hit(){
        anim.SetTrigger("Hit");
    }

    public void Restore(){
        anim.SetTrigger("Restore");
    }
}
