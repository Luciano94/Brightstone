using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SynopsisController : MonoBehaviour{
    [SerializeField] private Image xImg;
    [SerializeField] private RectTransform synopsisTxt;
    [SerializeField] private float txtSpeed;

    private bool isConnected = false;
    private const int loadingIndex = 3;

    private void Update(){
        DetectDevice();

        if (synopsisTxt.anchoredPosition.y < -10.0f){
            synopsisTxt.Translate(0.0f, txtSpeed * Time.deltaTime, 0.0f);
        }

        if (IsConnected){
            if(Input.GetButtonDown("Xattack"))
                StartRun();
        }
        
        xImg.enabled = IsConnected; 
    }

    public void StartRun(){
        SceneManager.LoadScene(loadingIndex);
    }

    private void DetectDevice(){
        if (Input.GetJoystickNames().Length > 0){
            if(Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0].Length > 10)
                isConnected = true;
            else
                isConnected = false;
        }
        else
            isConnected = false;
    }

    public bool IsConnected{
        get{return isConnected;}
    }
}
