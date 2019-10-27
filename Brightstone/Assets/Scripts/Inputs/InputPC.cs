using UnityEngine;

public class InputPC : IInput{
    public float GetVerticalAxis(){
        return Input.GetAxis("Vertical");
    }
    public float GetHorizontalAxis(){
        return Input.GetAxis("Horizontal");
    }
    public bool GetBasicAttackButton(){
        return Input.GetButtonDown("Fire1");
    }
    public bool GetStrongAttackButton(){
        return Input.GetButtonDown("Fire2");
    }
    public bool GetInteractButton(){
        return Input.GetButtonDown("Interact");
    }
    public bool GetActionButton(){
        return Input.GetButtonDown("Action");
    }
    public bool GetPauseButton(){
        return Input.GetButtonDown("Cancel");
    }
    public bool GetRestartButton(){
        return Input.GetButtonDown("Restart");
    }
    public bool GetPassButton(){
        return Input.GetButtonDown("Pass");
    }

        public bool GetActionZone(){
        return Input.GetButtonDown("Aattack");
    }

    
    public bool GetActionShuriken(){
        return Input.GetButtonDown("Battack");
    }

    
    public bool GetActionBeatdown(){
        return Input.GetButtonDown("Xattack");
    }

    
    public bool GetActionThrust(){
        return Input.GetButtonDown("Yattack");
    }

    
    public bool GetActionDash(){
        return Input.GetButtonDown("Dash");
    }

    public bool GetActionSimpleAttack(){
        return Input.GetButtonDown("RBattack");
    }
}