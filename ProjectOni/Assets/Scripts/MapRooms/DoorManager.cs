using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]private GameObject doorD;
    [SerializeField]private GameObject doorU;
    [SerializeField]private GameObject doorL;
    [SerializeField]private GameObject doorR;


    public void SetDoors(bool d, bool u, bool l, bool r){

        doorD.SetActive(!d);

        doorU.SetActive(!u);

        doorL.SetActive(!l);

        doorR.SetActive(!r);
    }

}
