using UnityEngine;

public class Autodestroy : MonoBehaviour{
    public float timeToDestroy;

    public Autodestroy(float timeToDestroy){
        this.timeToDestroy = timeToDestroy;
    }

    private void Update(){
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0.0f)
            Destroy(gameObject);
    }
}
