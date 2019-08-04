using UnityEngine;

public class Autodestroy : MonoBehaviour{
    private float timeToDestroy;

    private void Update(){
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0.0f)
            Destroy(gameObject);
    }

    public void PrepareValues(float time){
        timeToDestroy = time;
    }
}
