using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour{
    Animator anim;

    private void Awake(){
		anim = GetComponent<Animator>();
	}

	void Update(){
        anim.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        anim.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
	}

    public void SetAttackTrigger(int dir, float animIndex){
        anim.SetTrigger("Attacking");
        anim.SetFloat("Anim", animIndex);
        anim.SetFloat("Dir", (float)dir);
    }

    public void SetParryTrigger(int dir){
        anim.SetTrigger("Parry");
        anim.SetFloat("Dir", (float)dir);
    }

    public void Death(){
        anim.SetTrigger("Death");

        enabled = false;
    }

    public void Idle(){
        anim.SetFloat("VerticalSpeed",   0.0f);
        anim.SetFloat("HorizontalSpeed", 0.0f);
    }
}
