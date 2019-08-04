using UnityEngine;

public class VanishToDeath : MonoBehaviour{
    private float timeToStartVanish;
    private float timeToFinishVanish;
    private float timeLeft;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if (timeLeft >= 0.0f){
            if (timeToStartVanish >= 0.0f){
                timeToStartVanish -= Time.deltaTime;
            }
            else{
                timeLeft -= Time.deltaTime;
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, timeLeft / timeToFinishVanish);
            }
        }
    }

    public void PrepareValues(float timeToStart, float timeToFinish){
        timeToStartVanish = timeToStart;
        timeToFinishVanish = timeToFinish;
        timeLeft = timeToFinishVanish;
    }
}
