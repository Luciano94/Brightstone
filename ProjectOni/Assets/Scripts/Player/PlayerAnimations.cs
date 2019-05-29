using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour{
    Animator anim;

    private void Awake() {
		anim = GetComponent<Animator>();
	}

	void Update() {
        anim.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        anim.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
	}

    public void SetAttackTrigger(int dir, bool isStrong){
        if(!isStrong)
            anim.SetTrigger("Attacking");
        else
            anim.SetTrigger("StrongAtk");
        anim.SetInteger("Direction", dir);
    }

    public void SetParryTrigger(int dir){
        anim.SetTrigger("Parry");
        anim.SetInteger("Direction", dir);
    }

    public void Death(){
        anim.SetTrigger("Death");

        enabled = false;
    }
}
