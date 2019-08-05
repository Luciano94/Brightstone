using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private OptionsController optionsPanel;
    [SerializeField] private Button firstOption;

    private Animator pauseAnim;
    private bool pauseState = false;
    
    private void Awake(){
        pauseAnim = pausePanel.GetComponent<Animator>();
    }

    private void Update(){
        if(Input.GetButtonDown("Pause") && !GameManager.Instance.isTutorial){
            pauseState = !pauseState;
            MenuManager.Instance.StartMenu = pauseState;
            

            if (!pauseState && optionsPanel.gameObject.activeSelf)
                optionsPanel.DesactivateThis();

            if (pauseState){
                firstOption.Select();
                pauseAnim.SetTrigger("In");
            }
            else{
                pauseAnim.SetTrigger("Out");
            }
        }
    }
}
