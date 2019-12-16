using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour{
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject combos;

    [SerializeField] private Button controlsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Animator anim;

    public void ChangeState(bool state){
        if(state){
            exitButton.Select();
            controlsButton.Select();
        }else{
            DesactivateThis();
        }
    }

    public void DesactivateThis(){
        controls.SetActive(false);
    }

    public void ActivateControls(){
        controls.SetActive(!controls.activeSelf);
    }

    public void ActivateCombos(){
        combos.SetActive(!combos.activeSelf);
    }
}