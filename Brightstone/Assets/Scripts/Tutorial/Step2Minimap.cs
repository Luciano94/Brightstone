using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2Minimap : Step{
    [SerializeField] private GameObject minimapBack;
    [SerializeField] private GameObject minimapFront;

    [SerializeField] private float timeToFinish = 2.0f;

    public override void StepInitialize(){
        minimapBack.SetActive(true);
        minimapFront.SetActive(true);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        timeToFinish -= Time.deltaTime;
        
        if (timeToFinish <= 0.0f)
            finished = true;
    }
}