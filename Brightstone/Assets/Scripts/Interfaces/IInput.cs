interface IInput{
    bool IsJoystick();
    float GetVerticalAxis();
    float GetHorizontalAxis();
    bool GetBasicAttackButton();
    bool GetStrongAttackButton();
    bool GetInteractButton();
    bool GetActionButton();
    bool GetPauseButton();
    bool GetRestartButton();
    bool GetPassButton();
    bool GetActionZone();
    bool GetActionShuriken();
    bool GetActionBeatdown();
    bool GetActionThrust();
    bool GetActionDash();
    bool GetActionSimpleAttack();
}