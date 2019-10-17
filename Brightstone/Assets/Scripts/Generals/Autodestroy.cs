using UnityEngine;

public class Autodestroy : MonoBehaviour{
    public float timeToDestroy;

    private void Update(){
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0.0f)
            Destroy(gameObject);
    }
}
