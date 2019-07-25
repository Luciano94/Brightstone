using UnityEngine;

public class Step1Movement : Step{
    [SerializeField] private Transform destination;
    [SerializeField] private SpriteRenderer sprDestination;
    [SerializeField] private float minDistance;

    public override void StepInitialize(){
        sprDestination.enabled = true;
    }

    public override void StepFinished(){
        sprDestination.enabled = false;
    }

    public override void StepUpdate(){
        Vector3 diff = GameManager.Instance.PlayerPos - destination.position;
        diff.z = 0.0f;
        float dist = diff.magnitude;

        if (dist <= minDistance)
            finished = true;
    }
}