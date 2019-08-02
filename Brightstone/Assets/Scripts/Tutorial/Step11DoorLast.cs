using UnityEngine;

public class Step11DoorLast : Step{
    [SerializeField] private BoxCollider2D finalTrigger;
    [SerializeField] private TriggerAdvisor triggerAdvisor;
    private ActiveRoom aR;
    public override void StepInitialize(){
        //aR = GameManager.Instance.activeRoom;
        //aR.GetRoomsBehaviour().RoomFinished();
        //aR.GetNodeExits().OpenDoors();

        triggerAdvisor.OnTrigger.AddListener(OnFinalTrigger);
        finalTrigger.enabled = true;
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        
    }

    private void OnFinalTrigger(){
        finished = true;
    }
}
