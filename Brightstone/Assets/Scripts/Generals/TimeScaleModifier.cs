using UnityEngine;

public class TimeScaleModifier : MonoBehaviour{
    private static TimeScaleModifier instance;

    public static TimeScaleModifier Instance{
        get {
            instance = FindObjectOfType<TimeScaleModifier>();
            if(instance == null){
                GameObject go = new GameObject("Managers");
                instance = go.AddComponent<TimeScaleModifier>();
            }
            return instance;
        }
    }

    public int framesReduced;
    public float timeScaleReducedTo;

    private int framesLeftReduced;
    
    private void FixedUpdate(){
        if (framesLeftReduced > 0)
            if (--framesLeftReduced <= 0)
                Time.timeScale = 1.0f;
    }

    public void ReduceTimeScale(){
        Time.timeScale = timeScaleReducedTo;
        framesLeftReduced = framesReduced;
    }
}