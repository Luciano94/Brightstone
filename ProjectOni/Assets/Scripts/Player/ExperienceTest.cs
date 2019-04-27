using UnityEngine;

public class ExperienceTest : MonoBehaviour
{
    private PlayerExperience pExp;

    private void Start() {
        pExp = gameObject.GetComponent<PlayerExperience>();
    }
    private void Update() {
        if(Input.GetButton("Fire3")){
            pExp.Experience = 10;
        }
    }
}
