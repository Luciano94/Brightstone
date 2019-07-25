using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour{
    private static EventManager instance;

    public static EventManager Instance{
        get {
            instance = FindObjectOfType<EventManager>();
            if(instance == null){
                GameObject go = new GameObject("EventManager");
                instance = go.AddComponent<EventManager>();
            }
            return instance;
        }
    }

    [SerializeField] private Step[] steps;
    private int actualStep = 0;

    private void Update(){
        if (steps[actualStep].HadFinished()){
            steps[actualStep].StepFinished();
            actualStep++;

            if (actualStep >= steps.Length)
                TutorialFinished();
            else
                steps[actualStep].StepInitialize();
        }
        else{
            steps[actualStep].StepUpdate();
        }
    }

    private void TutorialFinished(){

    }
}
