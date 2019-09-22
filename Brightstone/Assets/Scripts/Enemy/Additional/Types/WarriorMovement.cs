using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : EnemyMovement{
    [Header("Strategies vars")]
    [SerializeField] private float distFromPlayer;
    override public void ApplyMovementStrategy(int chaserIndex){
        base.ApplyMovementStrategy(chaserIndex);

        switch(EnemyBehaviour.Instance.warriorStrategy){
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

            break;
            case Strategies.Melee22:

            break;
            case Strategies.Melee23:

            break;
            case Strategies.Melee24:

            break;

            // 3 enemies
            case Strategies.Melee31:
                Vector3 playerPos = GameManager.Instance.PlayerPos;
                Vector3 roomOrigin = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
                roomOrigin.z = playerPos.z;
                Vector3 dirPlayerToRoom = (playerPos - roomOrigin).normalized;

                if (chaserIndex == 0)
                    dirPlayerToRoom = Quaternion.AngleAxis(45.0f, Vector3.forward) * dirPlayerToRoom;
                else if (chaserIndex == 2)
                    dirPlayerToRoom = Quaternion.AngleAxis(-45.0f, Vector3.forward) * dirPlayerToRoom;

                MoveToObjective(playerPos + dirPlayerToRoom * distFromPlayer);
            break;
            case Strategies.Melee32:

            break;
            case Strategies.Melee33:

            break;
            case Strategies.Melee34:

            break;
            default:

            break;
        }
    }
}
