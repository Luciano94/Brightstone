using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour{
    [SerializeField] private GameObject controls;

    public void ActivateControls(){
        controls.SetActive(true);
    }
}
