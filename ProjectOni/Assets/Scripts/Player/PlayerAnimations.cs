using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    private void Awake() {
		anim = GetComponent<Animator>();
	}

	void Update() 
	{
        anim.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        anim.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
	}

    void SetAttackTrigger()
    {
        //anim.SetTrigger("Attacking");
        //anim.SetInteger("Direction", batScript.ActualDirection);
    }

    public void Death()
    {
        anim.SetTrigger("Death");

        enabled = false;
    }
}
