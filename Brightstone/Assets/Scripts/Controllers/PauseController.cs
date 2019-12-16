using UnityEngine;

public class PauseController : MonoBehaviour{
    [SerializeField] OptionsController oC;

    private bool pauseState = false;
    
    private void Update(){
        if (InputManager.Instance.GetPauseButton()){
            pauseState = !pauseState;
            GameManager.Instance.PauseGame(pauseState);
            
            oC.gameObject.SetActive(pauseState);
            oC.ChangeState(pauseState);
        }
    }

    private void SwitchPause(){
        GameManager.Instance.PauseGame(pauseState);
    }
}
