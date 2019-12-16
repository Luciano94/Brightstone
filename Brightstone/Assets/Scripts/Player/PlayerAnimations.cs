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
        if (GameManager.Instance.pause)
            return;

        if (InputManager.Instance.IsConnected){
            anim.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
            anim.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
        }
        else{
            anim.SetFloat("VerticalSpeed", Input.GetAxis("VerticalMouse"));
            anim.SetFloat("HorizontalSpeed", Input.GetAxis("HorizontalMouse"));
        }
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
        anim.SetFloat("Dir", Input.GetAxis("Horizontal"));

        SoundManager.Instance.PlayerDeath();

        EntityOrderManager.Instance.OnEntityDeath(GetComponent<SpriteRenderer>());

        enabled = false;
    }

    public void Idle(){
        anim.SetFloat("VerticalSpeed",   0.0f);
        anim.SetFloat("HorizontalSpeed", 0.0f);
    }
}
