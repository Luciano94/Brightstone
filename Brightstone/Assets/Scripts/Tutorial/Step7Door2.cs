public class Step7Door2 : Step{
    private ActiveRoom aR;

    public override void StepInitialize(){
        aR = GameManager.Instance.activeRoom;
        aR.GetRoomsBehaviour().RoomFinished();
        aR.GetNodeExits().OpenDoors();
    }

    public override void StepFinished(){
        aR.GetNodeExits().CloseDoors();
    }

    public override void StepUpdate(){
        if (!aR.GetRoomsBehaviour().Complete){
            aR.GetNodeExits().CloseDoors();
            finished = true;
        }
    }
}
