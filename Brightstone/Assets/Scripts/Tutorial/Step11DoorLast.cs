using UnityEngine;

public class Step11DoorLast : Step{
    [SerializeField] private BoxCollider2D finalTrigger;
    [SerializeField] private TriggerAdvisor triggerAdvisor;
    [SerializeField] private GameObject roomL;
    [SerializeField] private GameObject lastRoomArrow;

    [SerializeField] private NodeExits nodeExits;
    public override void StepInitialize(){

        nodeExits.CloseDoors();
        roomL.SetActive(true);
        lastRoomArrow.SetActive(true);
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
