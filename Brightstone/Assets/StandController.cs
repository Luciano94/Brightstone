using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandController : MonoBehaviour
{
    [SerializeField] private GameObject disableStand;
    [SerializeField] private GameObject enableStand;

    public void ToggleStand(bool active){
        enableStand.SetActive(active);
        disableStand.SetActive(!active);
    }
}
