using UnityEngine;

public class InputPC : IInput{
    public bool IsJoystick(){
        return false;
    }
    public float GetVerticalAxis(){
        return Input.GetAxis("VerticalMouse");
    }
    public float GetHorizontalAxis(){
        return Input.GetAxis("HorizontalMouse");
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
        return Input.GetButtonDown("Fire2");
    }
    public bool GetActionShuriken(){
        return Input.GetButtonDown("Battack");
    }
    public bool GetActionBeatdown(){
        return Input.GetButtonDown("Fire1");
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