using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour{
    [SerializeField] private GameObject controls;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button options;

    private bool paused = false;

    private void Update(){
        if (InputManager.Instance.GetPauseButton()){
            paused = !paused;
            if(paused){
                DesactivateThis();
            }else{
                controlsButton.Select();
                ActivateControls();
            }

        }
    }

    public void DesactivateThis(){
        if (controls.activeSelf)
            controls.SetActive(false);

        options.Select();

        gameObject.SetActive(false);
    }

    public void ActivateControls(){
        controls.SetActive(!controls.activeSelf);
    }

    private void OnEnable(){
        controlsButton.Select();
    }
}
