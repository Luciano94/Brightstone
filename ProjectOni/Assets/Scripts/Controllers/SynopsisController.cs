using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SynopsisController : MonoBehaviour{
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private float timeToAppear;
    [SerializeField] private Image xImg;
    [SerializeField] private RectTransform synopsisTxt;
    [SerializeField] private float txtSpeed;
    [SerializeField] private float timeMoving;

    private float timeLeft = 0.0f;
    private bool isConnected = false;
    private const int loadingIndex = 3;

    private void Update(){
        DetectDevice();

        timeLeft += Time.deltaTime;

        if (synopsisTxt.anchoredPosition.y < -10.0f){
            synopsisTxt.Translate(0.0f, txtSpeed * Time.deltaTime, 0.0f);
        }

        if (timeLeft >= timeToAppear){
            if (!skipBtn.activeSelf)
                skipBtn.SetActive(true);
            
            if (IsConnected)
                if(Input.GetButtonDown("Xattack"))
                    StartRun();

            xImg.enabled = IsConnected; 
        }
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
