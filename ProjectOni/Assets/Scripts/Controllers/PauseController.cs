using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    private bool pauseState = false;
    private bool optionsState = false;

    private void Update(){

        if(Input.GetButtonDown("Pause")){
            pauseState = !pauseState;
            pausePanel.SetActive(pauseState);
        }
        if(Input.GetButtonDown("Select") && pauseState){
            if(!optionsState)
                ActivateOptionsPanel();
            else{
                GameManager.Instance.ExitToMainMenu();
            }
        }
    }

    public void ActivateOptionsPanel(){
        optionsState = !optionsState;
        optionsPanel.SetActive(optionsState);
    }
}
