using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    private bool pauseState = false;
    private bool optionsState = false;

    private void Update(){

        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseState = !pauseState;
            pausePanel.SetActive(pauseState);
        }
    }

    public void ActivateOptionsPanel(){
        optionsState = !optionsState;
        optionsPanel.SetActive(optionsState);
    }
}
