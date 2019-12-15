using UnityEngine;

public class Action : MonoBehaviour {

    [SerializeField] private new string name;
    [SerializeField] private Animation anim;
    [SerializeField] private float dmg;
    [SerializeField] private float totalTime;
    [SerializeField] private PlayerCombat playerCombat;
    public Stands standToPlay;
    public Actions actionName;
    //float drag;
    //float stunt;
    private FrameData fData;
    private bool isActive = false;
    private float actualTime=0.0f;

    public float Damage{
        get{return dmg;}
    }

    private void Awake() {
        fData.enterFrames = ((30 * totalTime)/ 100);
        fData.activeFrames = ((60 * totalTime) / 100);
        fData.exitFrames = totalTime;
    }

    public void StartAction(ActionInfo actionInfo, float index){
        isActive = true;
        actualTime = 0.0f;
        fData.State = ActionState.enterFrames;
        playerCombat.StartAction(actionInfo, index);
    }

    public void ResetAction(ActionInfo actionInfo, int index){
        actualTime = 0.0f;
        fData.State = ActionState.enterFrames;
        playerCombat.StartAction(actionInfo, index);
    }

    public void StopAction(){
        isActive = false;
        actualTime = 0.0f;
        fData.State = ActionState.enterFrames;
        playerCombat.StopAction();
    }

    public float ActualTime{
        get{return actualTime;}
    }

    public bool IsActive{
        get{return isActive;}
    }

    public FrameData Fdata{
        get{return fData;}
    }

    private void Update() {
        if(isActive){
            actualTime += Time.deltaTime;
            if(actualTime >= fData.enterFrames){
                fData.State = ActionState.activeFrames;
            }
            if(actualTime >= fData.activeFrames + fData.enterFrames){
                fData.State = ActionState.exitFrames;
            }
            if(actualTime >= fData.exitFrames){
                fData.State = ActionState.enterFrames;
                actualTime = 0.0f;
                isActive = false;
            }
        }
    }

}
/* 
    private void Awake() {
        fData.enterFrames = ((30 * totalTime)/ 100);
        fData.activeFrames = ((40 * totalTime) / 100);
        fData.exitFrames = totalTime;
    }

    public void UpdateAction() {
        actualTime = playerCombat.ActualTime;
        fData.State = playerCombat.State;
        if(fData.State == ActionState.exitFrames){
            isActive = false;
        }
    }

    public ActionState State{
        get{return fData.State;}
    }

    public FrameData FData{
        get{return fData;}
    }

    public void StartAction(){
        fData.State = ActionState.enterFrames;
        actualTime = 0.0f;
        isActive = true;
        playerCombat.StartAttack(this);
    }

    public void StopAction(){
        isActive = false;
        playerCombat.StopAttack();
    }

    public void ResetAction(){
        fData.State = ActionState.enterFrames;
        actualTime = 0.0f;
        isActive = true;
        playerCombat.StartAttack(this);
    }

    public bool CanChain(){
        if(fData.State == ActionState.activeFrames){
            return true;
        }
        return false;
    }

*/