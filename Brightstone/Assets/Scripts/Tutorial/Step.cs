using UnityEngine;

public abstract class Step : MonoBehaviour{
    protected bool finished = false;

    virtual public void StepInitialize() {}
    virtual public void StepUpdate() {}
    virtual public void StepFinished() {}
    public bool HadFinished() { return finished; }
}