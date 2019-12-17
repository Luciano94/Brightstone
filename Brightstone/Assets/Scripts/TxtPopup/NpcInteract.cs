using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcInteract : MonoBehaviour
{
    [Header("Text Info")]
    [SerializeField] GameObject textCanvas;

    [SerializeField]private TextMeshPro textMesh;
    [SerializeField]private string[] textsToShow;
    private int actualText;
    private bool textIsShowed;

    [Header("Icon Info")]
    [SerializeField]private GameObject iconCanvas;
    private bool iconShown = false;
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;
        actualText = -1;
        textIsShowed = false;
        iconShown = false;
        iconCanvas.SetActive(false);
        textCanvas.SetActive(false);
    }

    private void Update() {
        DetectPlayer();
        if(iconShown){
            if(InputManager.Instance.GetInteractButton()){
                textIsShowed = true;
                iconCanvas.SetActive(false);
                textCanvas.SetActive(true);
                ShowText();
            }
        }
    }

    private void ShowText(){
        if(actualText == textsToShow.Length-1){
            actualText = 0;
        }else{
            actualText++;
        }
        textMesh.text = textsToShow[actualText];
    }

    private void DetectPlayer(){
        if(Vector3.Distance(transform.position, gameManager.PlayerPos) <= 5.0f){
            if(!textIsShowed){
                iconCanvas.SetActive(true);
                iconShown = true;
            }
        }else{
            iconCanvas.SetActive(false); 
            textCanvas.SetActive(false);
            iconShown = false;
            textIsShowed = false; 
        }
    }
    

}
