using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step10Market : Step{
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;
    
    private int textIndex = 0;
    private bool aguanteLaMerca = false;
    private bool firstDialogueFinished = false;

    public override void StepInitialize(){
        TextGenerator.Instance.Show(initialTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (!firstDialogueFinished){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    textIndex = 0;
                    firstDialogueFinished = true;
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            aguanteLaMerca = true;
            TextGenerator.Instance.Show(middleTexts[textIndex]);
        }

        if (!aguanteLaMerca) return;

        if (textIndex < middleTexts.Length){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        finished = true;
    }
}
