using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2Minimap : Step{
    [SerializeField] private GameObject minimapBack;
    [SerializeField] private GameObject minimapFront;
    [SerializeField] private GameObject minimapArrow;
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;

    public override void StepInitialize(){
        minimapBack.SetActive(true);
        minimapFront.SetActive(true);
        minimapArrow.SetActive(true);
        GameManager.Instance.DisablePlayer();
        TextGenerator.Instance.Appear();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (textIndex < initialTexts.Length){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    minimapArrow.SetActive(false);
                }
            }
            return;
        }

        finished = true;
    }
}