using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : EnemyMovement{
    [Header("Strategies Variables")]
    [SerializeField] private float distFromPlayer;
    [SerializeField] private float rotationSpeedStr33;

    private float currentAngleStr31;
    private float currentAngleStr33;
    private int chaserIndex;

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
        Vector3 dirPlayerFwd31 = Vector3.forward;

        if (chaserIndex == 2)
            dirPlayerFwd31 = Quaternion.AngleAxis(currentAngleStr31 + 225.0f, Vector3.forward) * dirPlayerFwd31;
        else
            dirPlayerFwd31 = Quaternion.AngleAxis(currentAngleStr31 + chaserIndex * 45.0f, Vector3.forward) * dirPlayerFwd31;

        Vector3 objectivePos31 = GameManager.Instance.PlayerPos + dirPlayerFwd31 * distFromPlayer;

        if ((objectivePos31 - transform.position).magnitude > 0.1f)
            MoveToObjective(objectivePos31);
    }

    public void RandomizeAngleStr31()
    {
        float newAngle = Random.Range(0.0f, 119.0f);
        currentAngleStr31 += newAngle * newAngle % 2 == 0 ? 1.0f : -1.0f;

        if (currentAngleStr31 >= 360.0f)
            currentAngleStr31 -= 360.0f;
        else if (currentAngleStr31 < 0.0f)
            currentAngleStr31 += 360.0f;
    }

    private void Melee32(){
        Vector3 playerPos32 = GameManager.Instance.PlayerPos;
        Vector3 roomOrigin32 = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
        roomOrigin32.z = playerPos32.z;
        Vector3 dirPlayerToRoom32 = (playerPos32 - roomOrigin32).normalized;

        if (chaserIndex == 0)
            dirPlayerToRoom32 = Quaternion.AngleAxis(45.0f, Vector3.forward) * dirPlayerToRoom32;
        else if (chaserIndex == 2)
            dirPlayerToRoom32 = Quaternion.AngleAxis(-45.0f, Vector3.forward) * dirPlayerToRoom32;

        Vector3 objectivePos32 = playerPos32 + dirPlayerToRoom32 * distFromPlayer;

        if ((objectivePos32 - transform.position).magnitude > 0.1f)
            MoveToObjective(objectivePos32);
    }

    private void Melee33(){
        currentAngleStr33 += rotationSpeedStr33 * Time.deltaTime;
        if (currentAngleStr33 >= 360.0f)
            currentAngleStr33 -= 360.0f;
        
        Vector3 dirPlayerFwd33 = Vector3.forward;

        dirPlayerFwd33 = Quaternion.AngleAxis(currentAngleStr33 + chaserIndex * 120.0f, Vector3.forward) * dirPlayerFwd33;

        Vector3 objectivePos33 = GameManager.Instance.PlayerPos + dirPlayerFwd33 * distFromPlayer;

        if ((objectivePos33 - transform.position).magnitude > 0.1f)
            MoveToObjective(objectivePos33);
    }

    private void Melee34(){
        Vector3 playerPos34 = GameManager.Instance.PlayerPos;
        Vector3 roomOrigin34 = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
        roomOrigin34.z = playerPos34.z;
        Vector3 dirPlayerToRoom34 = (playerPos34 - roomOrigin34).normalized;

        if (chaserIndex == 1)
            dirPlayerToRoom34 = Quaternion.AngleAxis(30.0f, Vector3.forward) * dirPlayerToRoom34;
        else if (chaserIndex == 2)
            dirPlayerToRoom34 = Quaternion.AngleAxis(-30.0f, Vector3.forward) * dirPlayerToRoom34;

        Vector3 objectivePos34 = playerPos34 + dirPlayerToRoom34 * distFromPlayer;

        if ((objectivePos34 - transform.position).magnitude > 0.1f)
            MoveToObjective(objectivePos34);
    }
}
