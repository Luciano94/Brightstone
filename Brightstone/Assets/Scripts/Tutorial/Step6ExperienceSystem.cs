using UnityEngine;

public class Step6ExperienceSystem : Step{
    [SerializeField] private GameObject experience; 

    [SerializeField] private float timeToFinish = 1.0f;

    public override void StepInitialize(){
        experience.SetActive(true);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        timeToFinish -= Time.deltaTime;

        if (timeToFinish <= 0.0f)
            finished = true;
    }
}
