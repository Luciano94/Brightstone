using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour{
    [SerializeField] private Step[] steps;
    private int actualStep = 0;
    const int loadingIndex = 3;

    private void Start(){
        Invoke("TutorialStart", 0.1f);
    }

    private void TutorialStart(){
        steps[actualStep].StepInitialize();
    }

    private void Update(){
        if (steps[actualStep].HadFinished()){
            steps[actualStep].StepFinished();
            actualStep++;

            if (actualStep < steps.Length)
                steps[actualStep].StepInitialize();
            else
                TutorialFinished();
        }
        else{
            steps[actualStep].StepUpdate();
        }
    }

    private void TutorialFinished(){
        SceneManager.LoadScene(loadingIndex);
    }
}