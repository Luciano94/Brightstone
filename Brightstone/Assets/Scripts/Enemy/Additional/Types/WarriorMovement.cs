using UnityEngine;

public class WarriorMovement : EnemyMovement{
    [Header("Strategies Variables")]
    [SerializeField] private float distFromPlayer;
    [SerializeField] private float rotationSpeedStr33;

    private float angleStr33;
    private int chaserIndex;

    private Vector3 playerPos;
    private float timeLeftToRefresh = 0;

    const float TIME_PER_REFRESH = 0.1f;
    const float DIST_LIMIT = 0.3f;

    private void Update(){
        playerPos = GameManager.Instance.PlayerPos;
        /*if (timeLeftToRefresh <= 0)
        {
            timeLeftToRefresh = TIME_PER_REFRESH;
            playerPos = GameManager.Instance.PlayerPos;
        }*/
    }

    override public void ApplyMovementStrategy(int chaserIndex){
        base.ApplyMovementStrategy(chaserIndex);

        if (chaserIndex == -1) return;

        this.chaserIndex = chaserIndex;

        switch(EnemyBehaviour.Instance.currentStrategy){
            // 1 enemy
            case Strategies.Melee11:

            break;
            case Strategies.Melee12:

            break;
            case Strategies.Melee13:

            break;
            case Strategies.Melee14:

            break;

            // 2 enemies
            case Strategies.Melee21:
                Melee31();
            break;
            case Strategies.Melee22:
                Melee32();
            break;
            case Strategies.Melee23:
                Melee33();
            break;
            case Strategies.Melee24:
                Melee34();
            break;

            // 3 enemies
            case Strategies.Melee31:
                Melee31();
            break;
            case Strategies.Melee32:
                Melee32();
            break;
            case Strategies.Melee33:
                Melee33();
            break;
            case Strategies.Melee34:
                Melee34();
            break;
        }
    }

    private void Melee31(){
        Vector3 obj = new Vector3();
        
        float angle = EnemyBehaviour.Instance.currentAngleStr31;

        if (chaserIndex == 2)
        {
            obj.x = playerPos.x + Mathf.Cos(angle + 225.0f) * distFromPlayer;
            obj.y = playerPos.y + Mathf.Sin(angle + 225.0f) * distFromPlayer;
        }
        else
        {
            obj.x = playerPos.x + Mathf.Cos(angle + chaserIndex * 45.0f) * distFromPlayer;
            obj.y = playerPos.y + Mathf.Sin(angle + chaserIndex * 45.0f) * distFromPlayer;
        }

        obj.z = playerPos.z;

        MakeMovement(obj);
    }

    private void Melee32(){
        Vector3 roomOrigin32 = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
        roomOrigin32.z = playerPos.z;
        float angle = Calculations.GetAngle(playerPos - roomOrigin32) - 45.0f;
        
        Vector3 obj = new Vector3();

        obj.x = playerPos.x + Mathf.Cos(angle + chaserIndex * 45.0f) * distFromPlayer;
        obj.y = playerPos.y + Mathf.Sin(angle + chaserIndex * 45.0f) * distFromPlayer;
        obj.z = playerPos.z;

        MakeMovement(obj);
    }

    private void Melee33(){
        angleStr33 += rotationSpeedStr33 * Time.deltaTime;
        if (angleStr33 >= 360.0f)
            angleStr33 -= 360.0f;
        
        Vector3 obj = new Vector3();

        obj.x = playerPos.x + Mathf.Cos(angleStr33 + chaserIndex * 120.0f) * distFromPlayer;
        obj.y = playerPos.y + Mathf.Sin(angleStr33 + chaserIndex * 120.0f) * distFromPlayer;
        obj.z = playerPos.z;

        MakeMovement(obj);
    }

    private void Melee34(){
        Vector3 roomOrigin34 = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
        roomOrigin34.z = playerPos.z;
        float angle = Calculations.GetAngle(playerPos - roomOrigin34);

        Vector3 obj = new Vector3();

        if (chaserIndex == 1)
        {
            obj.x = playerPos.x + Mathf.Cos(angle + 30.0f) * distFromPlayer;
            obj.y = playerPos.y + Mathf.Sin(angle + 30.0f) * distFromPlayer;
        }
        else if (chaserIndex == 2)
        {
            obj.x = playerPos.x + Mathf.Cos(angle - 30.0f) * distFromPlayer;
            obj.y = playerPos.y + Mathf.Sin(angle - 30.0f) * distFromPlayer;
        }
        else
        {
            obj.x = playerPos.x + Mathf.Cos(angle) * distFromPlayer;
            obj.y = playerPos.y + Mathf.Sin(angle) * distFromPlayer;
        }

        obj.z = playerPos.z;

        MakeMovement(obj);
    }

    void MakeMovement(Vector3 obj){
        if ((obj - transform.position).magnitude > DIST_LIMIT){
            eAnim.Move();
            MoveToObjective(obj);
        }
        else{
            eAnim.Idle();
        }
    }
}
