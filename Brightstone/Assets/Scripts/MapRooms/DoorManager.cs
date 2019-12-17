using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]private GameObject doorD;
    [SerializeField]private GameObject doorU;
    [SerializeField]private GameObject doorL;
    [SerializeField]private GameObject doorR;

    private bool left, right, up, down;

    public void SetDoors(bool d, bool u, bool l, bool r){

        doorD.SetActive(!d);
        down = d;

        doorU.SetActive(!u);
        up = u;

        doorL.SetActive(!l);
        left = l;

        doorR.SetActive(!r);
        right = r;
    }

    
    public void ActiveRoom(){
        doorD.SetActive(true);

        doorU.SetActive(true);

        doorL.SetActive(true);

        doorR.SetActive(true);
    }

    public void DesactiveRoom(){
        SetDoors(down,up,left,right);
    }

}
