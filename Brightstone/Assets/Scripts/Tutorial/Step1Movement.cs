﻿using UnityEngine;

public class Step1Movement : Step{
    [SerializeField] private Transform destination;
    [SerializeField] private SpriteRenderer sprDestination;
    [SerializeField] private float minDistance;
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;

    public override void StepInitialize(){
        sprDestination.enabled = true;
        GameManager.Instance.DisablePlayer();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }

    public override void StepFinished(){
        sprDestination.enabled = false;
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

        Vector3 diff = GameManager.Instance.PlayerPos - destination.position;
        diff.z = 0.0f;
        float dist = diff.magnitude;

        if (dist <= minDistance)
            finished = true;
    }
}