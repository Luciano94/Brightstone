using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour{
    Animator anim;

    private void Awake(){
		anim = GetComponent<Animator>();
	}

    private void Start(){
        EntityOrderManager.Instance.AddEntity(GetComponent<SpriteRenderer>());
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

    public void SetDashTrigger(int dir){
        anim.SetTrigger("Dash");
        anim.SetFloat("Dir", (float)dir);
    }

    public void Death(){
        anim.SetTrigger("Death");

        EntityOrderManager.Instance.OnEntityDeath(GetComponent<SpriteRenderer>());

        enabled = false;
    }

    public void Idle(){
        anim.SetFloat("VerticalSpeed",   0.0f);
        anim.SetFloat("HorizontalSpeed", 0.0f);
    }
}
