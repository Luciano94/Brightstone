using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour{
    [SerializeField] private GameObject controls;
    [SerializeField] private Button options;

    private void Update(){
        if (InputManager.Instance.GetPauseButton())
            DesactivateThis();
    }

    public void DesactivateThis(){
        if (controls.activeSelf)
            controls.SetActive(false);

        //options.Select();

        gameObject.SetActive(false);
    }

    public void ActivateControls(){
        controls.SetActive(true);
    }
}
