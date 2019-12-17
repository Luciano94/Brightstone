using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandsManager : MonoBehaviour
{
    [SerializeField]private StandController shurikenStand;
    [SerializeField]private StandController thrustStand;
    [SerializeField]private StandController beatdownStand;
    [SerializeField]private StandController zoneStand;


    public void ActivateStand(Stands stand){
      /*  switch (stand)
        {
            case Stands.Beatdown:
                shurikenStand.ToggleStand(false);
                thrustStand.ToggleStand(false);
                zoneStand.ToggleStand(false);
                beatdownStand.ToggleStand(true);
            break;
            case Stands.Shuriken:
                shurikenStand.ToggleStand(true);
                thrustStand.ToggleStand(false);
                zoneStand.ToggleStand(false);
                beatdownStand.ToggleStand(false);
            
            break;
            case Stands.Zone:
                shurikenStand.ToggleStand(false);
                thrustStand.ToggleStand(false);
                zoneStand.ToggleStand(true);
                beatdownStand.ToggleStand(false);
            break;
            case Stands.Thrust:
                shurikenStand.ToggleStand(false);
                thrustStand.ToggleStand(true);
                zoneStand.ToggleStand(false);
                beatdownStand.ToggleStand(false);
            break;
        }*/
    }

}
