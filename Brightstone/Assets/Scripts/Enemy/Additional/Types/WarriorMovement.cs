using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : EnemyMovement{
    override public void ApplyMovementStrategy(int chaserIndex){
        base.ApplyMovementStrategy(chaserIndex);

        switch(EnemyBahaviour.Instance.warriorStrategy){
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
