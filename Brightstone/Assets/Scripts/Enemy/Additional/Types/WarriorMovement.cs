using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : EnemyMovement{
    override public void ApplyMovementStrategy(int chaserIndex){
        base.ApplyMovementStrategy(chaserIndex);

        switch(EnemyBahaviour.Instance.warriorStrategy){
            // 1 enemy
            case WarriorStrategy.Melee11:

            break;
            case WarriorStrategy.Melee12:

            break;
            case WarriorStrategy.Melee13:

            break;
            case WarriorStrategy.Melee14:

            break;

            // 2 enemies
            case WarriorStrategy.Melee21:

            break;
            case WarriorStrategy.Melee22:

            break;
            case WarriorStrategy.Melee23:

            break;
            case WarriorStrategy.Melee24:

            break;

            // 3 enemies
            case WarriorStrategy.Melee31:

            break;
            case WarriorStrategy.Melee32:

            break;
            case WarriorStrategy.Melee33:

            break;
            case WarriorStrategy.Melee34:

            break;
            default:
            
            break;
        }
    }
}
