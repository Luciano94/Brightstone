using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private OptionsController optionsPanel;
    [SerializeField] private Button firstOption;
    [SerializeField] private Button controlButton;

    [Header("New Pause")]
    [SerializeField]private GameObject pauseMenu;

    private Animator pauseAnim;
    private bool pauseState = false;
    
    private void Awake(){
        pauseAnim = pausePanel.GetComponent<Animator>();
    }

    private void Update(){
        if (InputManager.Instance.GetPauseButton()){
            pauseState = !pauseState;
            pauseMenu.SetActive(pauseState);
            GameManager.Instance.PauseGame(pauseState);

        }
    }

    private void SwitchPause(){
        GameManager.Instance.PauseGame(pauseState);
    }
}
