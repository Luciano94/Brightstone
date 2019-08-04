using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private OptionsController optionsPanel;
    [SerializeField] private Button firstOption;

    private bool pauseState = false;
    
    private void Update(){
        if(Input.GetButtonDown("Pause") && !GameManager.Instance.isTutorial){
            pauseState = !pauseState;
            MenuManager.Instance.StartMenu = pauseState;
            firstOption.Select();

            if (!pauseState && optionsPanel.gameObject.activeSelf)
                optionsPanel.DesactivateThis();

            pausePanel.SetActive(pauseState);
        }
    }
}
