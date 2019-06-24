using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour{
    [SerializeField] private Image xImg;

    private void Update(){
        if (GameManager.Instance.IsConnected){
            xImg.enabled = true;
            if(Input.GetButtonDown("Xattack"))
                GameManager.Instance.ExitToMainMenu();
        }
        else
            xImg.enabled = false;
    }
}
