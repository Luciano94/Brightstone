using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2Minimap : Step{
    [SerializeField] private GameObject minimapBack;
    [SerializeField] private GameObject minimapFront;
    [SerializeField] private float timeToFinish = 0.15f;
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;

    public override void StepInitialize(){
        minimapBack.SetActive(true);
        minimapFront.SetActive(true);
        GameManager.Instance.DisablePlayer();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (textIndex < initialTexts.Length){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        timeToFinish -= Time.deltaTime;
        
        if (timeToFinish <= 0.0f)
            finished = true;
    }
}