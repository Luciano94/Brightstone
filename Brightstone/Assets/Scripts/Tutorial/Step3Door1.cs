using UnityEngine;

public class Step3Door1 : Step{
    [SerializeField] float timeToFinish = 2.0f;

    public override void StepInitialize(){
        
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        timeToFinish -= Time.deltaTime;

        if (timeToFinish <= 0.0f)
            finished = true;
    }
}
