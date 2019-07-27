using UnityEngine;

public class EventManager : MonoBehaviour{
    [SerializeField] private Step[] steps;
    private int actualStep = 0;

    private void Start(){
        steps[actualStep].StepInitialize();
    }

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